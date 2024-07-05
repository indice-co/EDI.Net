namespace indice.Edi.Utilities;


internal static class BufferUtils
{
    public static char[] RentBuffer(IArrayPool<char> bufferPool, int minSize) {
        if (bufferPool == null) {
            return new char[minSize];
        }

        char[] buffer = bufferPool.Rent(minSize);
        return buffer;
    }

    public static void ReturnBuffer(IArrayPool<char> bufferPool, char[] buffer) {
        if (bufferPool == null) {
            return;
        }

        bufferPool.Return(buffer);
    }

    public static char[] EnsureBufferSize(IArrayPool<char> bufferPool, int size, char[] buffer) {
        if (bufferPool == null) {
            return new char[size];
        }

        if (buffer != null) {
            bufferPool.Return(buffer);
        }

        return bufferPool.Rent(size);
    }
}
