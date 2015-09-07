using System;

namespace indice.Edi.Utilities
{

    internal class NameTable
    {
        // used to defeat hashtable DoS attack where someone passes in lots of strings that hash to the same hash code
        private static readonly int HashCodeRandomizer;

        private int _count;
        private Entry[] _entries;
        private int _mask = 31;

        static NameTable() {
            HashCodeRandomizer = Environment.TickCount;
        }

        public NameTable() {
            _entries = new Entry[_mask + 1];
        }

        public string Get(char[] key, int start, int length) {
            if (length == 0)
                return string.Empty;

            int hashCode = length + HashCodeRandomizer;
            hashCode += (hashCode << 7) ^ key[start];
            int end = start + length;
            for (int i = start + 1; i < end; i++) {
                hashCode += (hashCode << 7) ^ key[i];
            }
            hashCode -= hashCode >> 17;
            hashCode -= hashCode >> 11;
            hashCode -= hashCode >> 5;
            for (Entry entry = _entries[hashCode & _mask]; entry != null; entry = entry.Next) {
                if (entry.HashCode == hashCode && TextEquals(entry.Value, key, start, length))
                    return entry.Value;
            }

            return null;
        }

        public string Add(string key) {
            if (key == null)
                throw new ArgumentNullException("key");

            int length = key.Length;
            if (length == 0)
                return string.Empty;

            int hashCode = length + HashCodeRandomizer;
            for (int i = 0; i < key.Length; i++) {
                hashCode += (hashCode << 7) ^ key[i];
            }
            hashCode -= hashCode >> 17;
            hashCode -= hashCode >> 11;
            hashCode -= hashCode >> 5;
            for (Entry entry = _entries[hashCode & _mask]; entry != null; entry = entry.Next) {
                if (entry.HashCode == hashCode && entry.Value.Equals(key))
                    return entry.Value;
            }

            return AddEntry(key, hashCode);
        }


        private string AddEntry(string str, int hashCode) {
            int index = hashCode & _mask;
            Entry entry = new Entry(str, hashCode, _entries[index]);
            _entries[index] = entry;
            if (_count++ == _mask) {
                Grow();
            }
            return entry.Value;
        }

        private void Grow() {
            Entry[] entries = _entries;
            int newMask = (_mask * 2) + 1;
            Entry[] newEntries = new Entry[newMask + 1];

            for (int i = 0; i < entries.Length; i++) {
                Entry next;
                for (Entry entry = entries[i]; entry != null; entry = next) {
                    int index = entry.HashCode & newMask;
                    next = entry.Next;
                    entry.Next = newEntries[index];
                    newEntries[index] = entry;
                }
            }
            _entries = newEntries;
            _mask = newMask;
        }

        private static bool TextEquals(string str1, char[] str2, int str2Start, int str2Length) {
            if (str1.Length != str2Length)
                return false;

            for (int i = 0; i < str1.Length; i++) {
                if (str1[i] != str2[str2Start + i])
                    return false;
            }
            return true;
        }

        private class Entry
        {
            internal readonly string Value;
            internal readonly int HashCode;
            internal Entry Next;

            internal Entry(string value, int hashCode, Entry next) {
                Value = value;
                HashCode = hashCode;
                Next = next;
            }
        }
    }
}