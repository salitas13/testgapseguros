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
var AddClientComponent = /** @class */ (function () {
    function AddClientComponent(formBuilder, router, apiService) {
        this.formBuilder = formBuilder;
        this.router = router;
        this.apiService = apiService;
    }
    AddClientComponent.prototype.ngOnInit = function () {
        this.addForm = this.formBuilder.group({
            IdCliente: [],
            Nombres: ['', [forms_1.Validators.required, forms_1.Validators.minLength(50)]],
            Apellidos: ['', [forms_1.Validators.required, forms_1.Validators.minLength(50)]],
            FechaNacimiento: ['', forms_1.Validators.required],
            Cedula: ['', [forms_1.Validators.required, forms_1.Validators.minLength(10)]]
        });
    };
    AddClientComponent.prototype.onSubmit = function () {
        var _this = this;
        this.apiService.createClient(this.addForm.value)
            .subscribe(function (data) {
            if (data.status === 200) {
                alert('Cliente creado satisfactoriamente.');
                _this.router.navigate(['list-client']);
            }
            else {
                // Error en la validaci�n de la cobertura de la poliza mostrar mensaje al cliente
                alert('El porcentaje de cubrimiento no puede ser superior al 50% ya que el riesgo es Alto');
            }
        });
    };
    AddClientComponent = __decorate([
        core_1.Component({
            selector: 'app-add-client',
            templateUrl: './add-client.component.html',
            styleUrls: ['./add-client.component.css']
        }),
        __metadata("design:paramtypes", [forms_1.FormBuilder, router_1.Router, api_service_1.ApiService])
    ], AddClientComponent);
    return AddClientComponent;
}());
exports.AddClientComponent = AddClientComponent;
//# sourceMappingURL=add-client.component.js.map