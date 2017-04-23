using System;
using indice.Edi.FormatSpec;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// Use <see cref="EdiValueAttribute"/> for any value inside a segment. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class EdiValueAttribute : EdiAttribute
    {
        private IFormatSpec _formatSpec;
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

        public IFormatSpec FormatSpec {
            get { return _formatSpec; }
        }

        public EdiValueAttribute() { 
            _formatSpec = default(PictureSpec);

        }

        public EdiValueAttribute(string spec, FormatterType formatterType)
        {
            _formatSpec = FormatSpecFactory.Create(formatterType, spec);
        }
    }
}
