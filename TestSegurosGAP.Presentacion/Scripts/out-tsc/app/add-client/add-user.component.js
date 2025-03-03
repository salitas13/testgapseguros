"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var api_service_1 = require("../core/api.service");
var AddUserComponent = /** @class */ (function () {
    function AddUserComponent(formBuilder, router, apiService) {
        this.formBuilder = formBuilder;
        this.router = router;
        this.apiService = apiService;
    }
    AddUserComponent.prototype.ngOnInit = function () {
        this.addForm = this.formBuilder.group({
            id: [],
            username: ['', forms_1.Validators.required],
            password: ['', forms_1.Validators.required],
            firstName: ['', forms_1.Validators.required],
            lastName: ['', forms_1.Validators.required],
            age: ['', forms_1.Validators.required],
            salary: ['', forms_1.Validators.required]
        });
    };
    AddUserComponent.prototype.onSubmit = function () {
        var _this = this;
        this.apiService.createUser(this.addForm.value)
            .subscribe(function (data) {
            _this.router.navigate(['list-user']);
        });
    };
    AddUserComponent = __decorate([
        core_1.Component({
            selector: 'app-add-user',
            templateUrl: './add-user.component.html',
            styleUrls: ['./add-user.component.css']
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, router_1.Router, api_service_1.ApiService])
    ], AddUserComponent);
    return AddUserComponent;
}());
exports.AddUserComponent = AddUserComponent;
//# sourceMappingURL=add-user.component.js.map