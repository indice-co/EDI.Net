using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indice.Edi.Serialization
{
    /// <summary>
    /// <see cref="EdiPathAttribute"/> is used to specify the path. Path is similar to a relative uri. 
    /// ie DTM/0/1 or DTM/0 or even simply DTM
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class EdiPathAttribute : EdiAttribute
    {
        private EdiPath _Path;

        /// <summary>
        /// The path identifying the annotated members position inside the Edi transmission. 
        /// Expects a string representation of an <see cref="EdiPath"/> pointing to a structure ie: "XYZ" or "XYZ/0"
        /// </summary>
        public string Path {
            get { return _Path; }
            set { _Path = (EdiPath)value; }
        }

        internal EdiPath PathInternal {
            get { return _Path; }
        }

        /// <summary>
        /// The name of the <see cref="EdiContainerType.Segment"/>
        /// </summary>
        public string Segment {
            get {
                return _Path.Segment;
            }
        }

        /// <summary>
        /// Zero based index of the <see cref="EdiContainerType.Element"/> location inside the <seealso cref="EdiContainerType.Segment"/>
        /// </summary>
        public int ElementIndex {
            get {
                return _Path.ElementIndex;
            }
        }

        /// <summary>
        /// Zero based index of the <see cref="EdiContainerType.Component"/> location inside an <seealso cref="EdiContainerType.Element"/>
        /// </summary>
        public int ComponentIndex {
            get {
                return _Path.ComponentIndex;
            }
        }

        /// <summary>
        /// constructs the <see cref="EdiPathAttribute"/>
        /// </summary>
        /// <param name="path">Expects a string representation of an <see cref="EdiPath"/> pointing to a structure ie: "XYZ" or "XYZ/0"</param>
        public EdiPathAttribute(string path) 
            : this((EdiPath)path){

        }

        /// <summary>
        /// constructs the <see cref="EdiPathAttribute"/> given its position.
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="elementIndex"></param>
        /// <param name="componentIndex"></param>
        public EdiPathAttribute(string segment, int elementIndex, int componentIndex) 
            : this(new EdiPath(segment, elementIndex, componentIndex)) {

        }

        /// <summary>
        /// constructs the <see cref="EdiPathAttribute"/>.
        /// </summary>
        /// <param name="path"></param>
        public EdiPathAttribute(EdiPath path) {
            _Path = path;
        }
        
    }
}
