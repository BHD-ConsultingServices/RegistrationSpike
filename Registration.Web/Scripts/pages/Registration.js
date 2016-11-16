var screenEnum = { Register: "Register", GetStatus: "GetStatus", UnRegister: "UnRegister", UnSubscribe: "UnSubscribe" }
var resultCodeEnum = { Undefined: 0, Success: 1, PartialSuccess: 2, Failure: 3 }
var statusEnum = { Undefined: 0, Registered: 1, UnRegistered: 2}

site.pageFactory = function (initialData) {

    var objectFactory = function() {
        var getIdentityServerModel = function (identityNumber) {
            return {
                identityNumber: identityNumber
            }
        }

        var getRegisterServerModel = function (identityNumber, name, birthDay, isSubscribed, email) {
            return {
                IdentityNumber: identityNumber,
                Name: name,
                Birthday: birthDay,
                Subscribed: isSubscribed
            }
        }

        return {
            getIdentityServerModel: getIdentityServerModel,
            getRegisterServerModel: getRegisterServerModel
        }
    }();


    var dal = function (links) {
        var useStubs = true;

        var registerStub = function (parameters, onSuccess) {
            var response = {
                ResultCode: resultCodeEnum.Success,
                Data: {
                    Id: "BB64220E-5E11-49F1-8046-2400D8212B3E",
                    IdentityNumber: parameters.IdentityNumber,
                    Name: parameters.Name,
                    BirthDay: new Date(),
                    Subscribed: true
                }
            }

            onSuccess(response);
        }

        var unRegisterStub = function (parameters, onSuccess) {
            var response = {
                ResultCode: resultCodeEnum.Success,
                Data: {
                    Id: "BB64220E-5E11-49F1-8046-2400D8212B3E",
                    IdentityNumber: parameters.IdentityNumber,
                    Name: parameters.Name,
                    BirthDay: new Date(),
                    Subscribed: true
                }
            }

            onSuccess(response);
        }

        var unSubscribeStub = function (parameters, onSuccess) {
            var response = {
                ResultCode: resultCodeEnum.Success,
                Data: {
                    Id: "BB64220E-5E11-49F1-8046-2400D8212B3E",
                    IdentityNumber: parameters.IdentityNumber,
                    Name: parameters.Name,
                    BirthDay: new Date(),
                    Subscribed: true
                }
            }

            onSuccess(response);
        }

        var getRegisteredStateStub = function (parameters, onSuccess) {
            debugger;
            var response = {
                ResultCode: resultCodeEnum.Success,
                Data: statusEnum.Success
            }

            onSuccess(response);
        }


        
        var register = function (parameters, onSuccess) {
 
        }
      
        var unRegister = function (parameters, onSuccess) {

        }

        var unSubscribe = function (parameters, onSuccess) {
 
        }

        var getRegisteredState = function (parameters, onSuccess) {

        }

        return {
            register: useStubs ? registerStub : register,
            unRegister: useStubs ? unRegisterStub : unRegister,
            unSubscribe: useStubs ? unSubscribeStub : unSubscribe,
            getRegisteredState: useStubs ? getRegisteredStateStub : getRegisteredState
        }
    }(initialData.links);

    var viewModel = function() {
        var currentState = ko.observable(screenEnum.Register);
        var message = ko.observable(undefined);

        var registerModel = new function() {
            var identityNumber = ko.observable(undefined);
            var birthDay = ko.observable(undefined);
            var name = ko.observable(undefined);
            var isSubscribed = ko.observable(true);
            var email = ko.observable(undefined);

            return {
                identityNumber: identityNumber,
                name: name,
                birthDay: birthDay,
                isSubscribed: isSubscribed,
                email: email
            }
        }();

        var changePerspective = function (state) {
            console.log("Changing the state");
            console.log(state);
            message(undefined);

            currentState(state);
        }

        var registerOnSuccess = function (jsonResult) {
            console.log("Register Completed");
            console.log(jsonResult);
        }

        var unSubscribeSuccess = function (jsonResult) {
            console.log("Un-Subscribe Completed");
            console.log(jsonResult);
        }

        var unRegisterSuccess = function (jsonResult) {
            console.log("Un-Register Completed");
            console.log(jsonResult);
        }

        var getRegisteredStateSuccess = function (jsonResult) {
            console.log("Get registered state Completed");
            console.log(jsonResult);
        }

        var register = function () {
            var parameters = objectFactory.getRegisterServerModel(
                registerModel.identityNumber(),
                registerModel.name(),
                registerModel.birthDay(),
                registerModel.isSubscribed(),
                registerModel.email());

            dal.register(parameters, registerOnSuccess);
        }

        var unSubscribe = function() {
            var parameters = objectFactory.getIdentityServerModel(registerModel.identityNumber());
            dal.unSubscribe(parameters, unSubscribeSuccess);
        }

        var unRegister = function() {
            var parameters = objectFactory.getIdentityServerModel(registerModel.identityNumber());
            dal.unRegister(parameters, unRegisterSuccess);
        }

        var getRegisteredStatus = function() {
            var parameters = objectFactory.getIdentityServerModel(registerModel.identityNumber());
            dal.getRegisteredState(parameters, getRegisteredStateSuccess);
        }
        
        return {
            currentState: currentState,
            registerModel: registerModel,
            register: register,
            unSubscribe: unSubscribe,
            unRegister: unRegister,
            getRegisteredStatus: getRegisteredStatus,
            changePerspective: changePerspective,
            message: message
        }
    }();
    
    return {
        viewModel: viewModel
    }
};