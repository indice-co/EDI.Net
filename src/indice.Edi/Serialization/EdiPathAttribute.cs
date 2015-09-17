using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class EdiPathAttribute : EdiAttribute
    {
        private EdiPath _Path;

        public string Path {
            get { return _Path; }
            set { _Path = (EdiPath)value; }
        }

        internal EdiPath PathInternal {
            get { return _Path; }
        }

        public string Segment {
            get {
                return _Path.Segment;
            }
        }

        public int ElementIndex {
            get {
                return _Path.ElementIndex;
            }
        }

        public int ComponentIndex {
            get {
                return _Path.ComponentIndex;
            }
        }
        public EdiPathAttribute(string path) 
            : this((EdiPath)path){

        }

        public EdiPathAttribute(string segment, int elementIndex, int componentIndex) 
            : this(new EdiPath(segment, elementIndex, componentIndex)) {

        }

        public EdiPathAttribute(EdiPath path) {
            _Path = path;
        }
        
    }
}
