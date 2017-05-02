using System;

namespace indice.Edi.FormatSpec
{
    public static class FormatSpecFactory
    {
        public static IFormatSpec Create(FormatterType formatterType, string spec)
        {
            switch (formatterType) {
                case FormatterType.PictureSpec:
                    return PictureSpec.Parse(spec);
                    break;
                case FormatterType.EdifactSpec:
                    return EdifactSpec.Parse(spec);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(formatterType), formatterType, null);
            }
        }
    }
}