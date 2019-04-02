package domain.model;


import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.xml.bind.annotation.adapters.CollapsedStringAdapter;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Pharmacy {
    private int id;
    private String email;
    private String phoneNumber;
    private String name;
    private String username;
    private String password;
    private String location;
    private boolean isAdmin;
    private List<Order> orders;



    public Pharmacy()
    {
        this.orders = new ArrayList<>();
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }


    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public List<Order> getOrders() {
        return orders;
    }

    public void setOrders(List<Order> orderList) {
        this.orders = orderList;
    }


    public boolean hasOrders()
    {
        if (this.orders.isEmpty())
        {
            return false;
        }
        else
            return true;
    }

    public boolean getIsAdmin() {
        return isAdmin;
    }

    public void setIsAdmin(boolean admin) {
        isAdmin = admin;
    }

    @Override
    public String toString() {
        return "Pharmacy{" +
                "id=" + id +
                ", email='" + email + '\'' +
                ", phoneNumber='" + phoneNumber + '\'' +
                ", name='" + name + '\'' +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                ", location='" + location + '\'' +
                ", isAdmin=" + isAdmin +
                ", orders=" + orders +
                '}';
    }
}
