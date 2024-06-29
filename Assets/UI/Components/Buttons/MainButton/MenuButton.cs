using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuButton : VisualElement
{
    public new class UxmlFactory : UxmlFactory<MenuButton> {};

    private Button button => this.Q<Button>("button");
    private Label text => this.Q<Label>("text");

    public void Init(string text, Texture2D bg){
        this.text.text = text;
        this.button.style.backgroundImage = bg;
    }

    public MenuButton (string text){
        this.text.text = text;
    }

    public MenuButton(){
    }
}
