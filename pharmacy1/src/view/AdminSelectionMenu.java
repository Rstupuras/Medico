package view;

import controller.MedicoController;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class AdminSelectionMenu {

    private MedicoController controller;
    private Stage selectionMenu;



    public AdminSelectionMenu(MedicoController controller) {
        this.selectionMenu = new Stage();
        this.selectionMenu = selectionMenu();
        this.controller = controller;
    }

    private Stage selectionMenu() {
        Image image = new Image("images/logo.png");
        ImageView imageView = new ImageView();
        imageView.setImage(image);
        VBox vBox = new VBox();
        vBox.setAlignment(Pos.CENTER);
        HBox hBox = new HBox();
        hBox.setPrefWidth(800);
        hBox.setAlignment(Pos.CENTER);
        hBox.setSpacing(10);
        Button pharmacyButton = new Button("Pharmacies");
        Button medicamentsButton = new Button("Medicaments");
        pharmacyButton.setPrefSize(150,50);
        medicamentsButton.setPrefSize(150,50);
        hBox.getChildren().addAll(pharmacyButton, medicamentsButton);
        vBox.getChildren().addAll(imageView,hBox);

        pharmacyButton.setOnAction(event -> {
            controller.execute("pharmacyMenu");
        });

        medicamentsButton.setOnAction(event -> {
            controller.execute("medicamentsMenu");
        });

        Scene scene = new Scene(vBox, 700, 400);
        selectionMenu.setResizable(false);
        selectionMenu.setTitle("Medico");
        selectionMenu.setScene(scene);
        return selectionMenu;

    }

    public void showStage()
    {
        selectionMenu.show();
    }

    public void closeStage()
    {
        selectionMenu.close();
    }
}
