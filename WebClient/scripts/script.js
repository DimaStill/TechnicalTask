function  ViewModel() {
        var self = this;
        self.persons = ko.observableArray([]);
        self.showAddPerson = ko.observable(false);

        self.newGivenName = ko.observable('');
        self.newPrefixName = ko.observable('');
        self.newSuffixName = ko.observable('');
        self.newFamily = ko.observable('');
        self.newName = {
            family: self.newFamily,
            given: ko.computed(function() {
                return self.newGivenName().split(' ');
            }),
            prefix: ko.computed(function() {
                return self.newPrefixName().split(' ');
            }),
            suffix: ko.computed(function() {
                return self.newSuffixName().split(' ');
            }),
            text: ko.computed(function() {
                return `${self.newPrefixName()} ${self.newGivenName()} ${self.newFamily()} ${self.newSuffixName()}`
            }),
        };
        self.newPerson = {
            resourceType: 'Person',
            active: false,
            name: ko.observableArray([self.newName]),
            birthDate: ko.observable()
        };

        
        self.selectGivenName = ko.observable('');
        self.selectPrefixName = ko.observable('');
        self.selectSuffixName = ko.observable('');
        self.selectFamily = ko.observable('');
        self.selectName = {
            family: self.selectFamily,
            given: ko.computed(function() {
                return self.selectGivenName().split(' ');
            }),
            prefix: ko.computed(function() {
                return self.selectPrefixName().split(' ');
            }),
            suffix: ko.computed(function() {
                return self.selectSuffixName().split(' ');
            }),
            text: ko.computed(function() {
                return `${self.selectPrefixName()} ${self.selectGivenName()} ${self.selectFamily()} ${self.selectSuffixName()}`
            }),
        };
        self.selectPerson = {
            id: 0,
            resourceType: 'Person',
            active: false,
            name: ko.observableArray([self.selectName]),
            birthDate: ko.observable()
        };

        $.getJSON('https://localhost:5001/api/person', function(allData) {
            var mappedTasks = $.map(allData, function(item) { return new Person(item) });
            self.persons(mappedTasks);
            console.log(self.persons());
        });

        self.addPerson = function() {
            if(!self.validationAddInput()) {
                return;
            }
            var personInfo = ko.toJSON(self.newPerson);
            $.ajax({
                url: 'https://localhost:5001/api/person',
                data: personInfo,
                type: 'POST',
                contentType: "application/json",
                success: function(result) {
                    self.persons.push(result);
                    self.clearInput();
                }
            });
        }

        self.updatePerson = function() {
            if (!self.validationUpdateInput()) {
                return;
            }
            var personInfo = ko.toJSON(self.selectPerson);
            $.ajax({
                url: `https://localhost:5001/api/person/${self.selectPerson.id}`,
                data: personInfo,
                type: 'PUT',
                contentType: "application/json",
                success: function(result) {
                    let item = self.persons().find(x => x.id == result.id);
                    self.persons.replace(item, result);
                    $('.modal').modal('hide');
                }
            });
        }
        
        self.removePerson = function(person) {
            $.ajax(`https://localhost:5001/api/person/${person.id}`, {
                type: "delete", contentType: "application/json",
                success: function(result) { 
                    self.persons.remove(person); 
                }
            });
            event.stopPropagation(); 
        };

        self.displayAddPerson = function() {
            self.clearInput();
            self.showAddPerson(!self.showAddPerson());
            $('#date input').datepicker();
        }

        self.openPersonalInfoModal = function(person) {
            self.selectPerson.id = person.id;
            self.selectFamily(person.name[0].family);
            self.selectGivenName(person.name[0].given.join(' '));
            self.selectPrefixName(person.name[0].prefix.join(' '));
            self.selectSuffixName(person.name[0].suffix.join(' '));
            self.selectPerson.birthDate(person.birthDate);
        }

        self.validationAddInput = function() {
            let valid = true;
            if (self.newFamily() == '') {
                $('.family').addClass('is-invalid');
                valid = false;
            }
            if (self.newGivenName() == '') {
                $('.given-name').addClass('is-invalid');
                valid = false;
            }
            if (self.newPerson.birthDate() == '') {
                $('.birthday').addClass('is-invalid');
                valid = false;
            }
            return valid;
        }

        self.validationUpdateInput = function() {
            let valid = true;
            if (self.selectFamily() == '') {
                $('.update-family').addClass('is-invalid');
                valid = false;
            }
            if (self.selectGivenName() == '') {
                $('.update-given-name').addClass('is-invalid');
                valid = false;
            }
            if (self.newPerson.birthDate() == '') {
                $('.birthday').addClass('is-invalid');
                valid = false;
            }
            return valid;
        }

        self.clearInput = function() {
            self.newGivenName('');
            self.newPrefixName('');
            self.newSuffixName('');
            self.newFamily('');
        }

        $('#date input').datepicker();
}

Person = class {
    constructor(item) {
        Object.assign(this, item)
    }
    
    resourceType;
    active;
    name;
    birthDate;
}

ko.applyBindings(new ViewModel());