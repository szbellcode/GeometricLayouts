namespace Logic.Configuration
{
    /// <summary>
    /// Config values specific to application from appsettings.json
    /// </summary>
    public class AppConfig
    {
        public virtual int GridSize { get; set; }
        public virtual int GridSizeMin { get; set; }
        public virtual int GridSizeMax { get; set; }
        public virtual int NonHypotenuseSideLength { get; set; }
    }
}
