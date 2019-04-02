public class Response
{
    public string Name { get; set; }
    public int Number { get; set; }
    public Appointment Appointment { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
    public Order Order { get; set; }
    public OrderItem OrderItem { get; set; }
    public Prescription Prescription { get; set; }
    public Medicament Medicament { get; set; }
    public Pharmacy Pharmacy { get; set; }
}