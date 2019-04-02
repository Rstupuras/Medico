package domain.model;

import domain.mediator.MedicoModel;

public class Medicament {
    private int id;
    private boolean isPrescribed;
    private String name;
    private double price;
    private String description;



    public Medicament()
    {

    }

    public Medicament(String name, String description)
    {
        this.name=name;
        this.description=description;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public boolean getIsPrescribed() {
        return isPrescribed;
    }

    public void setIsPrescribed(boolean prescribed) {
        isPrescribed = prescribed;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    @Override
    public String toString() {
        return  "Name: " + name + "\n" +
                "Price: " + price;
    }
}
