package domain.mediator;


import domain.model.Medicament;
import domain.model.Order;
import domain.model.Pharmacy;

import java.util.ArrayList;
import java.util.Collection;

public interface MedicoModel {

    void changeOrderStatusToNegative(Order order);

    void changeOrderStatusToCompleted(Order order);

    Collection<Order> getOrderList();

    Collection<Order> getAllUndoneOrders();

    Pharmacy getPharmacyByUsernameAndPassword(String username, String password);

    void editPharmacy(Pharmacy pharmacy);

    void addPharmacy(Pharmacy pharmacy);

    Collection<Pharmacy> getAllPharmacies();

    void deletePharmacy(Pharmacy pharmacy);

    void addMedicament(Medicament medicament);

    Collection<Medicament> getAllMedicaments();

    boolean doesMedicamentCanBeDeleted(Medicament medicament);

    void deleteMedicament(Medicament medicament);

    void editMedicament(Medicament medicament);

}
