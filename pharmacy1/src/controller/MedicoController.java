package controller;


import domain.mediator.MedicoModel;
import domain.model.Pharmacy;
import view.MedicoView;

import java.sql.SQLException;

public class MedicoController {
    private MedicoModel model;
    private MedicoView view;

    public MedicoController(MedicoModel model, MedicoView view) {
        this.model = model;
        this.view = view;

    }

    public void execute(String what) {
        switch (what) {
            case "loadUndoneOrders":
                view.setTableUndoneOrders(model.getAllUndoneOrders());
                break;
            case "allOrdersMenu":
                view.showAllOrdersMenu();
                break;
            case "loadAllOrders":
                view.setTableAllOrders(model.getOrderList());
                break;
            case "Refresh":
                model.getPharmacyByUsernameAndPassword(view.getUsername(), view.getPassword());
                view.setTableUndoneOrders(model.getAllUndoneOrders());
                break;
            case "login":
                Pharmacy pharmacy1 =null;
                pharmacy1 = model.getPharmacyByUsernameAndPassword(view.getUsername(),view.getPassword());
                if (pharmacy1 != null) {
                    if (pharmacy1.getIsAdmin() == false) {
                        view.showMainMenu();
                        view.closeLoginMenu();
                        break;
                    } else {
                        view.wrongLogin();
                    }
                }
                else
                {
                    view.wrongLogin();
                }
                break;
            case "loginAsAdmin":
                Pharmacy pharmacy =null;
                pharmacy = model.getPharmacyByUsernameAndPassword(view.getUsername(),view.getPassword());
                if (pharmacy != null)
                {
                    if (pharmacy.getIsAdmin() == true)
                    {
                        view.showSelectionMenu();
                        view.closeLoginMenu();
                    }
                    else {
                        view.wrongLogin();
                    }

                }
                else
                {
                    view.wrongLogin();
                }

                break;
            case "pharmacyMenu":
                if (view.getUsername()!=null)
                {

                    view.showAdminMenu();
                    view.closeLoginMenu();
                    view.setTableAllPharmacies(model.getAllPharmacies());
                    break;
                }
                else
                {
                    view.wrongLogin();
                }
                break;
            case "RefreshPharmacies":
                view.setTableAllPharmacies(model.getAllPharmacies());
                break;
            case "editPharmacy":
                model.editPharmacy(view.getEditedPharmacy());
                break;
            case "addNewPharmacy":
                model.addPharmacy(view.getNewPharmacy());
                break;
            case "medicamentsMenu":
                view.showMedicamentsMenu();
                break;
            case "loadAllMedicaments":
                view.setTableAllMedicaments(model.getAllMedicaments());
                break;
            case "addNewMedicament":
                model.addMedicament(view.getNewMedicament());
                break;
            case "RefreshMedicaments":
                view.setTableAllMedicaments(model.getAllMedicaments());
                break;
            case "deletePharmacy":
                model.deletePharmacy(view.getSelectedPharmacy());
                break;
            case "setOrderCompleted":
                model.changeOrderStatusToCompleted(view.getSelectedOrderToChangeStatus());
                break;
            case "setOrderDenied":
                model.changeOrderStatusToNegative(view.getSelectedOrderToChangeStatus());
                break;
            case "checkIfMedicamentCanBeDeleted":
                view.canMedicamentBeDeleted(model.doesMedicamentCanBeDeleted(view.getSelectedMedicament()));
                break;
            case "deleteMedicament":
                model.deleteMedicament(view.getSelectedMedicament());
                break;
            case "editMedicament":
                model.editMedicament(view.getEditedMedicament());
                break;
            default:
                System.out.println("default execute");
                break;
        }

    }

}
