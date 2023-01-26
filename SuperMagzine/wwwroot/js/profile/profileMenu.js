const ProfileViews = { INDEX: 'Index', PERSONAL: 'Personal' };

function GetView(viewName) {
    $('#menu_content').load('/Profile/GetView', { viewName: viewName });
}