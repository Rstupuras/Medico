import com.google.gson.Gson;
import controller.MedicoController;
import domain.mediator.MedicoModel;
import domain.mediator.MedicoModelManager;
import domain.model.Medicament;
import domain.model.Order;
import domain.model.OrderItem;
//import domain.model.OrderList;
import javafx.application.Application;
import javafx.stage.Stage;
import org.codehaus.jackson.type.TypeReference;
import view.GuiMedico;
import view.MedicoView;

import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.Entity;
import javax.ws.rs.core.Response;
import java.util.ArrayList;
import java.util.List;


public class Main extends Application {
    public static void main(String[] args) {
        launch(args);

    }

    @Override
    public void start(Stage primaryStage) {

        MedicoView view = new GuiMedico();
        MedicoModel model = new MedicoModelManager("localhost", "5001");
        MedicoController controller = new MedicoController(model, view);
        view.start(controller);
    }

}
