package domain.mediator;

import domain.model.Medicament;
import domain.model.Order;
import domain.model.Pharmacy;

import java.util.Collection;
import java.util.List;

public interface MedicoWebAPIConsumer {
    void changeOrderStatusToNegative(Order order);

    void changeOrderStatusToCompleted(Order order);

    Pharmacy getPharmacyByUsernameAndPassword(String username, String password);

    void editPharmacy(Pharmacy pharmacy);

    void addPharmacy(Pharmacy pharmacy);

    List<Pharmacy> getAllPharmacies();

    void deletePharmacy(Pharmacy pharmacy);

    void addMedicament(Medicament medicament);

    List<Medicament> getAllMedicaments();

    void deleteMedicament(Medicament medicament);

    void editMedicament(Medicament medicament);

}
