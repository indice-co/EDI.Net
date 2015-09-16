using System;

namespace indice.Edi.Serialization
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EdiValueAttribute : EdiAttribute
    {
        private Picture _picture;
        private bool _Mandatory;
        private string _Description;
        private string _Format;
        private string _Path;

        public bool Mandatory {
            get { return _Mandatory; }
            set { _Mandatory = value; }
        }

        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }

        public string Format {
            get { return _Format; }
            set { _Format = value; }
        }

        public string Path {
            get { return _Path; }
            set { _Path = value; }
        }

        public Picture Picture {
            get { return _picture; }
        }
        
        public EdiValueAttribute(string picture)
            : this((Picture)picture) {
        }

        public EdiValueAttribute(Picture picture) {
            _picture = picture;
        }
        
    }
}
