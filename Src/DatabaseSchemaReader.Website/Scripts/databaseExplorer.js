function ConnectionViewModel(databaseTypes) {
    var self = this;
    self.databaseTypes = databaseTypes.Data;
    
    self.databaseTypeSelectedItem = ko.observable("");
    self.hideNotAccessControls    = ko.computed(function() {
        return self.databaseTypeSelectedItem() != "Access";
    }),
    
    self.integratedSecurityTicked = ko.observable(false);
    self.hideCredentialControls   = ko.computed(function() {
        return !self.integratedSecurityTicked();
    });
}
