package view;

import controller.MedicoController;
import domain.model.Medicament;
import domain.model.Order;
import domain.model.Pharmacy;


import java.util.Collection;
import java.util.List;

public class GuiMedico implements MedicoView {
    private MedicoController controller;
    private MainPharmacyMenu mainMenu;
    private AllOrdersMenu allOrdersMenu;
    private LoginMenu loginMenu;
    private AdminPharmacyMenu adminMenu;
    private AdminMedicamentsMenu medicamentsMenu;
    private AdminSelectionMenu selectionMenu;

    @Override
    public void start(MedicoController controller) {
        this.controller = controller;
        this.mainMenu = new MainPharmacyMenu(controller);
        this.allOrdersMenu = new AllOrdersMenu(controller);
        this.loginMenu = new LoginMenu(controller);
        this.adminMenu = new AdminPharmacyMenu(controller);
        this.medicamentsMenu = new AdminMedicamentsMenu(controller);
        this.selectionMenu = new AdminSelectionMenu(controller);
        showLoginMenu();

    }

    @Override
    public void showLoginMenu() {
        loginMenu.showStage();
    }

    @Override
    public void closeLoginMenu() {
        loginMenu.closeStage();
    }

    @Override
    public void wrongLogin() {
        loginMenu.wrongLogin();
    }

    @Override
    public void showAdminMenu() {
        adminMenu.showStage();
    }

    @Override
    public Pharmacy getEditedPharmacy() {
        return adminMenu.getEditedPharmacy();
    }

    @Override
    public Pharmacy getNewPharmacy() {
        return adminMenu.getNewPharmacy();
    }

    @Override
    public Pharmacy getSelectedPharmacy() {
        return adminMenu.getSelectedPharmacy();
    }

    @Override
    public void setTableAllPharmacies(Collection<Pharmacy> pharmacyList) {
        adminMenu.setPharmacyItems((List<Pharmacy>) pharmacyList);
    }

    @Override
    public void setTableUndoneOrders(Collection<Order> orderList) {
        mainMenu.setUndoneOrderItems(orderList);
    }

    public void setTableAllOrders(Collection<Order> orderList)
    {
        allOrdersMenu.setAllOrderItems(orderList);
    }

    @Override
    public void showMainMenu(){
        mainMenu.showStage();
        controller.execute("loadUndoneOrders");
    }


    @Override
    public void showAllOrdersMenu() {

        controller.execute("loadAllOrders");
        allOrdersMenu.showStage();
    }

    @Override
    public Order getSelectedOrder() {
        return allOrdersMenu.getSelectedOrder();
    }

    public Order getSelectedOrderToChangeStatus()
    {
        return mainMenu.getSelectedOrderToChangeStatus();
    }

    @Override
    public void showMedicamentsMenu() {
        controller.execute("loadAllMedicaments");
        medicamentsMenu.showStage();
    }

    @Override
    public void setTableAllMedicaments(Collection<Medicament> medicamentList) {
        medicamentsMenu.setMedicamentsItems((List<Medicament>) medicamentList);
    }

    @Override
    public Medicament getNewMedicament() {
       return medicamentsMenu.getNewMedicament();
    }

    @Override
    public void showSelectionMenu() {
        selectionMenu.showStage();
    }

    @Override
    public Medicament getSelectedMedicament() {
      return medicamentsMenu.getSelectedMedicament();
    }

    @Override
    public void canMedicamentBeDeleted(Boolean b) {
        medicamentsMenu.setCanBeDeleted(b);
    }

    @Override
    public Medicament getEditedMedicament() {
        return medicamentsMenu.getEditedMedicament();
    }


    @Override
    public String getUsername()
    {
        return loginMenu.getUsername();
    }

    @Override
    public String getPassword()
    {
        return loginMenu.getPassword();
    }

}
