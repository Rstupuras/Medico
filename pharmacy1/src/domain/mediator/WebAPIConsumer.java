package domain.mediator;

import com.google.gson.Gson;
import domain.model.*;
import org.codehaus.jackson.map.ObjectMapper;
import org.codehaus.jackson.type.TypeReference;
import org.glassfish.jersey.client.authentication.HttpAuthenticationFeature;

import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.Entity;
import javax.ws.rs.core.Response;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;


    public class WebAPIConsumer implements MedicoWebAPIConsumer {

        private String url;
        private Login login;
        public WebAPIConsumer(String url){
            this.url=url;
            this.url = url;

        }

        @Override
        public void changeOrderStatusToNegative(Order order) {
            order.setStatus("Failed");
            Client client = ClientBuilder.newClient();
            Gson gson1 = new Gson();
            String orderJson = gson1.toJson(order);
            Response response1 = client.target(url +"/api/order/" + order.getId() + "?Patient=" + order.getPatientID()).request().accept("application/json; charset=utf-8").put(Entity.json(orderJson));
            System.out.println(response1.getStatus());
            response1.close();
        }

        @Override
        public void changeOrderStatusToCompleted(Order order) {
            order.setStatus("Completed");
            Client client = ClientBuilder.newClient();
            Gson gson1 = new Gson();
            String orderJson = gson1.toJson(order);
            Response response1 = client.target(url +"/api/order/" + order.getId() + "?Patient=" + order.getPatientID()).request().accept("application/json; charset=utf-8").put(Entity.json(orderJson));
            System.out.println(response1.getStatus());
            response1.close();
        }


        @Override
        public Pharmacy getPharmacyByUsernameAndPassword(String username, String password)
        {
            HttpAuthenticationFeature feature = HttpAuthenticationFeature
                    .basicBuilder().build();
            Client client = ClientBuilder.newClient();
                client.register(feature);
                Response response = client.target(url +"/api/pharmacy/login").request().property(
                        HttpAuthenticationFeature.HTTP_AUTHENTICATION_BASIC_USERNAME,
                        username)
                        .property(
                                HttpAuthenticationFeature.HTTP_AUTHENTICATION_BASIC_PASSWORD,
                                password).accept("application/json; charset=utf-8").post(Entity.json(null));
                System.out.println(response.getStatus());
                if (response.getStatus() == 400)
                {
                    return null;
                }
                org.codehaus.jackson.map.ObjectMapper mapper = new org.codehaus.jackson.map.ObjectMapper();
                mapper.configure(org.codehaus.jackson.map.DeserializationConfig.Feature.FAIL_ON_UNKNOWN_PROPERTIES,false);
                List<Pharmacy> pharmacy= response.readEntity(List.class);
                List<Pharmacy> ppharmacy = mapper.convertValue(
                        pharmacy,
                        new TypeReference<List<Pharmacy>>() { });
                response.close();
                return ppharmacy.get(0);

        }


        @Override
        public void editPharmacy(Pharmacy pharmacy) {
            Client client = ClientBuilder.newClient();
            Gson gson1 = new Gson();
            String pharmacyJson = gson1.toJson(pharmacy);
            Response response1 = client.target(url +"/api/pharmacy/" + pharmacy.getId()).request().accept("application/json; charset=utf-8").put(Entity.json(pharmacyJson));
            System.out.println(response1.getStatus());
            response1.close();
        }

        @Override
        public void addPharmacy(Pharmacy pharmacy) {
            Client client = ClientBuilder.newClient();
            Gson gson1 = new Gson();
            String pharmacyJson = gson1.toJson(pharmacy);
            Response response1 = client.target(url +"/api/pharmacy").request().accept("application/json; charset=utf-8").post(Entity.json(pharmacyJson));
            System.out.println(response1.getStatus());
            response1.close();
        }

        @Override
        public List<Pharmacy> getAllPharmacies() {
            Client client = ClientBuilder.newClient();
            org.codehaus.jackson.map.ObjectMapper mapper = new org.codehaus.jackson.map.ObjectMapper();
            mapper.configure(org.codehaus.jackson.map.DeserializationConfig.Feature.FAIL_ON_UNKNOWN_PROPERTIES,false);
            Response response = client.target(url + "/api/pharmacy").request()
                .accept("application/json; charset=utf-8").get();
            List<Pharmacy> pharmacies = response.readEntity(List.class);
            List<Pharmacy> pojos = mapper.convertValue(
                pharmacies,
                new TypeReference<List<Pharmacy>>() { });
            response.close();
            List<Pharmacy> pharmacyList = new ArrayList<>();
            for (int i = 0; i < pojos.size(); i++) {
            pharmacyList.add(pojos.get(i));
                }
            return pharmacyList;
        }

        @Override
        public void deletePharmacy(Pharmacy pharmacy) {
            Client client = ClientBuilder.newClient();
            Response response1 = client.target(url +"/api/pharmacy/" + pharmacy.getId()).request().accept("application/json; charset=utf-8").delete();
            System.out.println(response1.getStatus());
            response1.close();
        }

        @Override
        public void addMedicament(Medicament medicament) {
            Client client = ClientBuilder.newClient();
            Gson gson1 = new Gson();
            String medicamentJson = gson1.toJson(medicament);
            Response response1 = client.target(url +"/api/medicament").request().accept("application/json; charset=utf-8").post(Entity.json(medicament));
            System.out.println(response1.getStatus());
            response1.close();

        }

        @Override
        public List<Medicament> getAllMedicaments() {
            Client client = ClientBuilder.newClient();
            org.codehaus.jackson.map.ObjectMapper mapper = new org.codehaus.jackson.map.ObjectMapper();
            mapper.configure(org.codehaus.jackson.map.DeserializationConfig.Feature.FAIL_ON_UNKNOWN_PROPERTIES,false);
            Response response = client.target(url + "/api/medicament").request()
                    .accept("application/json; charset=utf-8").get();
            List<Medicament> medicaments = response.readEntity(List.class);
            List<Medicament> pojos = mapper.convertValue(
                    medicaments,
                    new TypeReference<List<Medicament>>() { });
            response.close();
            List<Medicament> medicamentList = new ArrayList<>();
            for (int i = 0; i < pojos.size(); i++) {
                medicamentList.add(pojos.get(i));
            }

            return medicamentList;
        }

        @Override
        public void deleteMedicament(Medicament medicament) {
            Client client = ClientBuilder.newClient();
            Response response1 = client.target(url +"/api/medicament/" + medicament.getId()).request().accept("application/json; charset=utf-8").delete();
            System.out.println(response1.getStatus());
            response1.close();
        }

        @Override
        public void editMedicament(Medicament medicament) {
            Client client = ClientBuilder.newClient();
            Gson gson1 = new Gson();
            String medicamentJson = gson1.toJson(medicament);
            Response response1 = client.target(url +"/api/medicament/" + medicament.getId()).request().accept("application/json; charset=utf-8").put(Entity.json(medicamentJson));
            System.out.println(response1.getStatus());
            response1.close();
        }

    }