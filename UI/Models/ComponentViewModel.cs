namespace UI.Models
{
    public class ComponentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Producer { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public override int GetHashCode()
        {
            return Id;
        }
        public override bool Equals(object obj)
        {
            if (obj is ComponentViewModel)
            {
                var c = (ComponentViewModel)obj;
                if (c.GetHashCode() == this.GetHashCode())
                {
                    return true;
                }
            }
            return false;
        }
    }
}