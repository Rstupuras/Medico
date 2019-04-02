package domain.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.sun.org.apache.xpath.internal.operations.Or;


import java.util.ArrayList;
import java.util.List;
@JsonIgnoreProperties(ignoreUnknown = true)
public class Order {
    private int id;
    private int patientID;
    private String status;
    private String orderNumber;
    private boolean isSent;
    private List<OrderItem> items;
    public Order()
    {
        items = new ArrayList<>();

    }

    public int getPatientID() {
        return patientID;
    }

    public void setPatientID(int patientID) {
        this.patientID = patientID;
    }


    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }


    public void setItems(List<OrderItem> orderItems) {
        this.items = orderItems;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getOrderNumber() {
        return orderNumber;
    }

    public void setOrderNumber(String orderNumber) {
        this.orderNumber = orderNumber;
    }

    public List<OrderItem> getItems() {
        return items;
    }

    public boolean getIsSent() {
        return isSent;
    }

    public void setIsSent(boolean sent) {
        isSent = sent;
    }
    @Override
    public String toString() {
        return "Order{" +
                "id=" + id +
                ", patientId=" + patientID +
                ", status='" + status + '\'' +
                ", orderNumber='" + orderNumber + '\'' +
                ", orderItems=" + items +
                '}';
    }
}
