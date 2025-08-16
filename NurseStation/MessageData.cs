namespace NurseStation
{
    public class MessageData
    {
        public string Question { get; set; } = "";
        public bool? Bath { get; set; } = false;
        public bool? Eat { get; set; } = false;
        public bool? Move { get; set; } = false;
        public bool? Drip { get; set; } = false;
        public bool? Drug { get; set; } = false;
        public double Mind { get; set; }
    }
}
