function QueryBuilder() {
    return this;
}

function Table(tableName, columns) {
    this.Name = tableName;
    this.Columns = columns;
    return this;
}

function Column(columnName) {
    this.Name = columnName;
    return this;
}

var listOfTables = [
    new Table("FirstTable", new Array(new Column("FirstTable_FirstColumn"), new Column("FirstTable_SecondColumn"))),
    new Table("SecondTable", new Array(new Column("SecondTable_FirstColumn"), new Column("SecondTable_SecondColumn"))),
    new Table("ThirdTable", new Array(new Column("ThirdTable_FirstColumn"), new Column("ThirdTable_SecondColumn"))),
    new Table("FourthTable", new Array(new Column("FourthTable_FirstColumn"), new Column("FourthTable_SecondColumn"))),
    new Table("FifthTable", new Array(new Column("FifthTable_FirstColumn"), new Column("FifthTable_SecondColumn")))
];
    
//var viewModelTables = {
    //tables: ko.observableArray(listOfPeople),
  //  selectedTables: ko.observableArray()
//};
    
//ko.applyBindings(viewModel);






function Person(id,name,age) {
    this.id = id;
    this.name = name;
    this.age = age;
}

var listOfPeople = [
    new Person(1, 'Fred', 25),
    new Person(2, 'Joe', 60),
    new Person(3, 'Sally', 43)
];

var viewModel = {
    people: ko.observableArray(listOfPeople),
    selectedPeople: ko.observableArray()
};
        
$(document).ready(function () {
    ko.applyBindings(viewModel);
});
