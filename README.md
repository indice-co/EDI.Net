# EDI.Net
EDI Parser/Deserializer.

At the moment working for Tradacoms/EDI Fact formats. 

Using attributes you can express all EDI rules like Mandatory/Conditional Segments,Elements & Components 
as well as describe component values with the picture syntax (e.g `9(3)`, `9(10)V9(2)` and `X(3)`).

#### Road map (TODO)

1. Update samples with Order9 and utility bill for tradacoms.
2. Implement serializer `Serialize` to write Clr classes to edi format (Using attributes).  

_Disclaimer. The project was ispired and heavily influenced by the work done in the excellent library JSON.Net by James Newton King_