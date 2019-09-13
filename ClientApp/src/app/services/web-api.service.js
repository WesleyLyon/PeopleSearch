"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//@Injectable({
//  providedIn: 'root'
//})
var WebApiService = /** @class */ (function () {
    function WebApiService(httpClient) {
        this.httpClient = httpClient;
        this.baseUrl = window.location.origin;
    }
    WebApiService.prototype.getPeople = function () {
        var _this = this;
        return this.httpClient.get(this.baseUrl + 'api/SampleData/GetPeople').subscribe(function (result) {
            _this.persons = result;
        }, function (error) { return console.error(error); });
    };
    return WebApiService;
}());
exports.WebApiService = WebApiService;
//# sourceMappingURL=web-api.service.js.map