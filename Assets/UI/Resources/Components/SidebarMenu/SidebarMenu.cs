
using UnityEngine;
using UnityEngine.UIElements;
public class SidebarMenu : VisualElement
{
    public new class UxmlFactory : UxmlFactory<SidebarMenu> { };

    VisualElement sidebarMenu;
    Button sidebarMenuControl;
    VisualElement sidebarMenuControlIcon;
    bool isSidebarMenuOpen = true;

    public SidebarMenu()
    {
        var asset = Resources.Load<VisualTreeAsset>("Components/SidebarMenu/SidebarMenu");
        asset.CloneTree(this);

        sidebarMenu = this.Q<VisualElement>("sidebar-menu");
        sidebarMenuControl = this.Q<Button>("sidebar-menu-control");
        sidebarMenuControlIcon = this.Q<VisualElement>("sidebar-menu-control-icon");

        this.AddToClassList("full-height");

        sidebarMenuControl.RegisterCallback<ClickEvent>(ev => ToggleSidebarMenu());
    }

    public void ToggleSidebarMenu()
    {
        if (isSidebarMenuOpen)
        {
            sidebarMenu.style.left = -sidebarMenu.layout.width;
            sidebarMenuControlIcon.AddToClassList("rotate-180");
            isSidebarMenuOpen = false;
        }
        else
        {
            sidebarMenu.style.left = 0;
            sidebarMenuControlIcon.RemoveFromClassList("rotate-180");
            isSidebarMenuOpen = true;
        }
    }
}