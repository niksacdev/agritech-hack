namespace SoilIngestion.Entity 
{
    public class Soil 
    {
        public string FarmerId { get; set; }

        public string FarmId { get; set; }

        public string Region { get; set; }

        public string TimeStamp { get; set; }

        public int Moisture { get; set; }

        public int Ph { get; set; }

        public int Thickness { get; set; }

        public double Mbv { get; set; }

        public int Pi { get; set; }

        public int Kpa { get; set; }

        public int Friction { get; set; }
    }
}