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
var router_1 = require("@angular/router");
var api_service_1 = require("../core/api.service");
var http_1 = require("@angular/common/http");
var ListInsuranceComponent = /** @class */ (function () {
    function ListInsuranceComponent(router, activatedRoute, apiService) {
        this.router = router;
        this.activatedRoute = activatedRoute;
        this.apiService = apiService;
    }
    ListInsuranceComponent.prototype.ngOnInit = function () {
        var _this = this;
        if (!window.localStorage.getItem('token')) {
            this.router.navigate(['login']);
            return;
        }
        this.sub = this.activatedRoute.queryParamMap.subscribe(function (params) {
            _this.clientId = params.get("clientId");
            if (!_this.clientId) {
                alert("Invalid action.");
                _this.router.navigate(['list-client']);
                return;
            }
            _this.apiService.getInsurancesClient(+_this.clientId) // (+) converts string 'insuranceId' to a number
                .subscribe(function (data) {
                _this.insurances = data.result;
            }, function (error) {
                if (error instanceof http_1.HttpErrorResponse) {
                    switch (error.status) {
                        case 0: //login
                            window.localStorage.removeItem("token");
                            window.localStorage.removeItem("editClientId");
                            window.localStorage.removeItem("editInsuranceId");
                            _this.router.navigate(['login']);
                            break;
                        case 401: //login
                            window.localStorage.removeItem("token");
                            window.localStorage.removeItem("editClientId");
                            window.localStorage.removeItem("editInsuranceId");
                            _this.router.navigate(['login']);
                            break;
                        case 403: //
                            window.localStorage.removeItem("token");
                            window.localStorage.removeItem("editClientId");
                            window.localStorage.removeItem("editInsuranceId");
                            _this.router.navigate(['login']);
                            break;
                    }
                }
            });
        });
    };
    ListInsuranceComponent.prototype.editEnsurance = function (insurance) {
        window.localStorage.removeItem("editInsuranceId");
        window.localStorage.setItem("editInsuranceId", insurance.IdPoliza.toString());
        this.router.navigate(['edit-insurance']);
    };
    ;
    ListInsuranceComponent.prototype.addInsurance = function () {
        this.router.navigate(['add-insurance'], { queryParams: { clientId: this.clientId.toString() } });
    };
    ;
    ListInsuranceComponent = __decorate([
        core_1.Component({
            selector: 'app-list-poliza',
            templateUrl: './list-insurance.component.html',
            styleUrls: ['./list-insurance.component.css']
        }),
        __metadata("design:paramtypes", [router_1.Router, router_1.ActivatedRoute, api_service_1.ApiService])
    ], ListInsuranceComponent);
    return ListInsuranceComponent;
}());
exports.ListInsuranceComponent = ListInsuranceComponent;
//# sourceMappingURL=list-insurance.component.js.map