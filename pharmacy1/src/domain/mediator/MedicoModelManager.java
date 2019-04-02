package domain.mediator;

import domain.model.Medicament;
import domain.model.Order;
import domain.model.Pharmacy;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;


public class MedicoModelManager implements MedicoModel {
    private MedicoWebAPIConsumer model;
    private Pharmacy pharmacy;



    public MedicoModelManager(String ip, String port) {
        String url = "https://" + ip +":" +port;
        model = new WebAPIConsumer(url);
    }


    @Override
    public void changeOrderStatusToCompleted(Order order) {
        model.changeOrderStatusToCompleted(order);
    }


    @Override
    public void changeOrderStatusToNegative(Order order) {
        model.changeOrderStatusToNegative(order);
    }


    @Override
    public List<Order> getOrderList() {
        List<Order> tmp = new ArrayList<>();
        for (int i = 0; i < pharmacy.getOrders().size(); i++) {
            if (pharmacy.getOrders().get(i).getIsSent() == true)
            {
                tmp.add(pharmacy.getOrders().get(i));
            }
        }
        return tmp;
    }

    public List<Order> getAllUndoneOrders()
    {
        List<Order> tmp = new ArrayList<>();
        for (int i = 0; i < pharmacy.getOrders().size(); i++) {
            String status = pharmacy.getOrders().get(i).getStatus();
            if (pharmacy.getOrders().get(i).getIsSent() == true) {
                if (status.equals("Not Completed")) {
                    tmp.add(pharmacy.getOrders().get(i));
                }
            }
        }
        return tmp;
    }

    @Override
    public Pharmacy getPharmacyByUsernameAndPassword(String username, String password) {
        this.pharmacy = model.getPharmacyByUsernameAndPassword(username, password);
       return model.getPharmacyByUsernameAndPassword(username,password);
    }

    @Override
    public void editPharmacy(Pharmacy pharmacy) {
        model.editPharmacy(pharmacy);
    }

    @Override
    public void addPharmacy(Pharmacy pharmacy) {
        model.addPharmacy(pharmacy);
    }

    @Override
    public List<Pharmacy> getAllPharmacies() {
        return model.getAllPharmacies();
    }

    @Override
    public void deletePharmacy(Pharmacy pharmacy) {
        model.deletePharmacy(pharmacy);
        if(pharmacy.getId() == this.pharmacy.getId())
        {
            System.exit(0);
        }

    }

    @Override
    public void addMedicament(Medicament medicament) {
        model.addMedicament(medicament);
    }

    @Override
    public Collection<Medicament> getAllMedicaments() {
        return model.getAllMedicaments();
    }

    public boolean doesMedicamentCanBeDeleted(Medicament medicament)
    {
        List<Pharmacy> pharmacyList = getAllPharmacies();
        List<Order> orderList = new ArrayList<>();
        int tmp = 0;
        if ( medicament.getIsPrescribed() == true)
        {
            return false;
        }
        else {
            for (int i = 0; i < pharmacyList.size(); i++) {
                for (int j = 0; j < pharmacyList.get(i).getOrders().size(); j++) {
                    orderList.add(pharmacyList.get(i).getOrders().get(j));
                }
            }
            for (int i = 0; i < orderList.size(); i++) {
                for (int j = 0; j < orderList.get(i).getItems().size(); j++) {
                    if (orderList.get(i).getItems().get(j).getMedicament().equals(medicament)) {
                        tmp += 1;
                    }
                }
            }

            if (tmp > 0) {
                return false;
            } else {
                return true;
            }
        }

    }

    @Override
    public void deleteMedicament(Medicament medicament) {
        model.deleteMedicament(medicament);
    }

    @Override
    public void editMedicament(Medicament medicament) {
        model.editMedicament(medicament);
    }
}
