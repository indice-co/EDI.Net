# EDI.Net
EDI Parser/Deserializer. A ground up implementation and does not make use of `XML Serialization` in any step of the process. This reduces the overhead of converting into multiple formats allong whe way of getting the desired Clr object.

At the moment working for Tradacoms/EDI Fact formats. 

Using attributes you can express all EDI rules like Mandatory/Conditional Segments,Elements & Components 
as well as describe component values with the picture syntax (e.g `9(3)`, `9(10)V9(2)` and `X(3)`).

#### Roadmap (TODO)

1. Update samples with Order9 and UtilityBill for tradacoms.
2. Showcase more cases using the EDIFact format.
4. Implement serializer `Serialize` to write Clr classes to edi format (Using attributes).  
5. Consider adding support for X12 format.

_Disclaimer. The project was inspired and influenced by the work done in the excellent library [JSON.Net](https://github.com/JamesNK/Newtonsoft.Json) by James Newton King. Some utility parts for reflection string parsing etc. are used as is_