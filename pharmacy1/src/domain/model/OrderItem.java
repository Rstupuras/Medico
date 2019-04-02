package domain.model;

public class OrderItem {
    private int id;
    private Medicament medicament;
    private int quantity;
    private int orderId;



        public OrderItem(int id, Medicament medicament, int quantity, int orderId) {
        this.id = id;
        this.medicament = medicament;
        this.quantity = quantity;
        this.orderId=orderId;
    }

    public OrderItem()
    {

    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public Medicament getMedicament() {
        return medicament;
    }

    public void setMedicament(Medicament medicament) {
        this.medicament = medicament;
    }

    public int getQuantity() {
        return quantity;
    }

    public void setQuantity(int quantity) {
        this.quantity = quantity;
    }

    public int getOrderId() {
        return orderId;
    }

    public void setOrderId(int orderId) {
        this.orderId = orderId;
    }

    @Override
    public String toString() {
        return medicament + "\n" +
                "Quantity: " + quantity;
    }
}
