var stateEnum = {Register: "Register", GetStatus: "GetStatus"}

site.pageFactory = function (initialData) {
    var objectFactory = function(links) {
        var register = function(parms, onSuccess) {
            
        }

        var unRegister = function (parms, onSuccess) {

        }

        var unSubscribe = function (parms, onSuccess) {

        }

        var getRegisteredState = function (parms, onSuccess) {

        }

        return {
            register: register,
            unRegister: unRegister,
            unSubscribe: unSubscribe,
            getRegisteredState: getRegisteredState
        }
    }(initialData.links);

    var viewModel = function() {
        var state = ko.observable(stateEnum.Register);
        var register = new function() {
            var identityNumber = ko.observable(undefined);

            return {
                identityNumber: identityNumber
            }
        }();
        
        return {
            state: state,
            register: register
        }
    }();
    
    return {
        objectFactory: objectFactory,
        viewModel: viewModel
    }
};