# Innovoft.Text.StringDecodingTrees
Innovoft.Text.StringDecodingTrees is a library for efficient string decoding. When decoding the same string multiple times using StringDecodingTree takes a third of the time than Encoding.GeString. Not to mention the GC benifit of not creating the same string over and over.