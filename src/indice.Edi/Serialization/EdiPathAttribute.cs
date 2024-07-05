namespace indice.Edi.Serialization;

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

    internal EdiPath PathInternal => _Path;

    /// <summary>
    /// The name of the <see cref="EdiContainerType.Segment"/>
    /// </summary>
    public string Segment => _Path.Segment;

    /// <summary>
    /// Zero based index of the <see cref="EdiContainerType.Element"/> location inside the <seealso cref="EdiContainerType.Segment"/>
    /// </summary>
    public int ElementIndex => _Path.ElementIndex;

    /// <summary>
    /// Zero based index of the <see cref="EdiContainerType.Component"/> location inside an <seealso cref="EdiContainerType.Element"/>
    /// </summary>
    public int ComponentIndex => _Path.ComponentIndex;

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
    /// <param name="segmentPart"></param>
    /// <param name="elementPart"></param>
    /// <param name="componentPart"></param>
    public EdiPathAttribute(string segmentPart, string elementPart, string componentPart) 
        : this(new EdiPath(new EdiPathFragment(segmentPart), new EdiPathFragment(elementPart), new EdiPathFragment(componentPart))) {

    }

    /// <summary>
    /// constructs the <see cref="EdiPathAttribute"/>.
    /// </summary>
    /// <param name="path"></param>
    public EdiPathAttribute(EdiPath path) {
        _Path = path;
    }

    /// <summary>
    /// String representation for the <see cref="EdiPathAttribute"/>
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Path = {_Path.ToString("o")}";
}
