package view;
import domain.model.*;
import controller.MedicoController;

import java.util.ArrayList;
import java.util.Collection;

public interface MedicoView {
    void start(MedicoController controller);
    void setTableUndoneOrders(Collection<Order> orderList);
    void setTableAllOrders(Collection<Order> orderList);
    void showMainMenu();
    void showAllOrdersMenu();
    Order getSelectedOrder();
    String getUsername();
    String getPassword();
    void showLoginMenu();
    void closeLoginMenu();
    void wrongLogin();
    void showAdminMenu();
    Pharmacy getEditedPharmacy();
    Pharmacy getNewPharmacy();
    Pharmacy getSelectedPharmacy();
    void setTableAllPharmacies(Collection<Pharmacy> pharmacyList);
    Order getSelectedOrderToChangeStatus();
    void showMedicamentsMenu();
    void setTableAllMedicaments(Collection<Medicament> medicamentList);
    Medicament getNewMedicament();
    void showSelectionMenu();
    Medicament getSelectedMedicament();
    void canMedicamentBeDeleted(Boolean b);
    Medicament getEditedMedicament();


}
