namespace BarcodeScanner.Mobile
{
    public class OCRResult
    {
        public bool Success { get; set; }

        public string AllText { get; set; } = "";

        public IList<Block> Blocks { get; set; } = [];
    }

    public class BaseOcr(string text, double confidence, List<Point> cornerPoints, double rotationDegree)
    {
        public string Text { get; set; } = text;
        public double Confidence { get; set; } = confidence;
        public List<Point> CornerPoints { get; set; } = cornerPoints;
        public double RotationDegree { get; set; } = rotationDegree;
    }
    public class Block(string text, List<Point> cornerPoints, List<Line> lines)
    {
        public string Text { get; set; } = text;
        public List<Point> CornerPoints { get; set; } = cornerPoints;
        public List<Line> Lines { get; set; } = lines;
    }

    public class Line(string text, double confidence, List<Point> cornerPoints, double rotationDegree, List<Element> elements)
        : BaseOcr(text, confidence, cornerPoints, rotationDegree)
    {
        public List<Element> Elements { get; set; } = elements;
    }

    public class Element(string text, double confidence, List<Point> cornerPoints, double rotationDegree)
        : BaseOcr(text, confidence, cornerPoints, rotationDegree)
    {
    }
}
