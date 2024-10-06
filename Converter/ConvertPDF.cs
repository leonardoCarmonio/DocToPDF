using Spire.Doc;

public static class ConvertPDF
{
    public static void Convert(string filePath)
    {
        try
        {
            var document = new Document();
            document.LoadFromFile(filePath);

            string filePdf = Path.ChangeExtension(filePath, ".pdf");
            document.SaveToFile(filePdf, FileFormat.PDF);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"""
                                Erro ao converter para PDF.
                                Message: {exception.Message} 
                                StackTrace: {exception.StackTrace}
                            """);
        }
    }
}