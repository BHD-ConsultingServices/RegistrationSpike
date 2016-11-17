/*
	This Knowlout validation Library is from the PayM8 Spikes.
	https://github.com/PayM8/FE-Knockout-Validation	
*/

$.validation = function () {
    var registeredFields = ko.observableArray([]);
    var registeredGroups = ko.observableArray([]);
    var hasPageError = ko.observable(false);
    var showPageMessages = ko.observable(false);
    var dateFormat = "DD/MM/YYYY";

    var createConditionalMandatoryParameter = function (condition, conditionalObservable, validationGroup) {
        var isFunction = false;
        var returnsBoolean = false;

        if (typeof condition === "function") {
            isFunction = true;
            returnsBoolean = typeof condition() === "boolean";
        }

        if (!isFunction || !returnsBoolean) {

            throw "Conditional Madatory Condition provided is not a function that returns a boolean value.";
        }
        if (conditionalObservable !== undefined && typeof conditionalObservable !== "function") {
            throw "For consditional validation include the onbservable responsible for changing a field to mandatory.";
        }

        var conditionAction = ko.computed(condition);

        return {
            condition: conditionAction,
            conditionalObservable: conditionalObservable,
            validationGroup: validationGroup
        }
    };

    var createConditionalDateParameter = function (isGreaterThan, isInclusive, creteria, customErrorMsg, validationGroup) {
        if (creteria === undefined || creteria === null) {
            creteria = moment(new Date()).startOf("day").toDate();
        }

        var hasDateCreteria = creteria instanceof Date;
        var hasCustomFunctionCreteria = typeof creteria === "function" && typeof creteria() === "boolean";
        var hasDateObservableCreteria = typeof creteria === "function" && (creteria() === undefined || (typeof creteria() === "object" && creteria() instanceof Date));
       
        if (typeof isGreaterThan != "boolean") {
            throw "Create Date Validation: Must supply isGreaterThan (when false isSmallerThan will be used) as a boolean";
        }
        if (typeof isInclusive != "boolean") {
            throw "Create Date Validation: Must supply isInclusive (Should it be greater/smaller than AND equal to) as a boolean";
        }

        if (!hasDateCreteria && !hasDateObservableCreteria && !hasCustomFunctionCreteria) {
            throw "For Create Date Validation you need to supply a creteria. It can be a Date object, an Observable Date Object or a function that return a boolean value";
        }

        return {
            isGreaterThan: isGreaterThan,
            isInclusive: isInclusive,
            hasDateCreteria: hasDateCreteria,
            hasDateObservableCreteria: hasDateObservableCreteria,
            hasCustomFunctionCreteria: hasCustomFunctionCreteria,
            creteria: creteria,
            customErrorMsg: (customErrorMsg === "") ? undefined : customErrorMsg,
            validationGroup: validationGroup
        }
    }
    
    var hasErrorInArray = function (observables) {
        for (var k = observables.length - 1; k >= 0; k--) {
            if (observables[k].hasError()) {
                return true;
            }
        }
        return false;
    };

    var concatinateStringArray = function (items) {
        var msg = "";
        for (var k = items.length - 1; k >= 0; k--) {
            msg = msg + items[k] + ((k <= 0) ? "" : "; ");
        }
        return msg;
    }


    var getDateObjectFromStringOrDate = function(dateObject)
    {
        if (dateObject === undefined || dateObject === null || dateObject === "") {
            return undefined;
        }

        if (typeof dateObject === "object") {
            if (!(dateObject instanceof Date)) {
                return undefined;
            }

            return dateObject;
        }

        if (typeof dateObject === "string") {
            return moment(dateObject, $.validation.dateFormat).toDate();
        }

        return undefined;
    }

    var viewAllErrors = function (groupName) {
        var errors = [];

        if (!groupName && hasErrorInArray(registeredFields)) {
            console.log("There are page validation errors");

            for (var k = registeredFields.length - 1; k >= 0; k--) {
                if (registeredFields[k].hasError()) {
                    errors.push(concatinateStringArray(registeredFields[k].errors()));
                };
            }
        }

        // View all group errors
        if (registeredGroups() != undefined && registeredGroups().length > 0) {
            for (var n = registeredGroups().length - 1; n >= 0; n--) {
                var group = registeredGroups()[n];

                if (groupName && group.name !== groupName) {
                    continue;
                }

                if (hasErrorInArray(group.registeredFields())) {
                    console.log("The validation group " + group.name + "contains validation errors");

                    for (var m = group.registeredFields().length - 1; m >= 0; m--) {
                        var field = group.registeredFields()[m];

                        if (field.hasError()) {
                            errors.push(concatinateStringArray(field.errors()));
                        };
                    }
                }
            }
        }

        return (errors.length > 0) ? errors : "No validation errors found.";
    };

    var getValidationGroupByName = function (groupName) {
        for (var k = registeredGroups().length - 1; k >= 0; k--) {
            if (registeredGroups()[k].name === groupName) return registeredGroups()[k];
        }

        return undefined;
    };

    var registerValidationField = function (model) {
        if (model.validationGroup === undefined || model.validationGroup === null) {
            registeredFields().push(model);
            return;
        }

        var group = getValidationGroupByName(model.validationGroup);
        if (group !== undefined && group !== null) {
            group.registeredFields().push(model);
            return;
        }

        var newValidationGroup = {
            name: model.validationGroup,
            showMessages: ko.observable(false),
            registeredFields: ko.observableArray([model])
        };

        registeredGroups().push(newValidationGroup);
    };

    var validatePage = function () {
        var isValid = true;
        if (hasErrorInArray(registeredFields())) isValid = false;

        return hasPageError(!isValid);
    };

    var registerValidation = function (model) {
        if (model.validationGroup === undefined || model.validationGroup === null) {
            model.subscribe(validatePage);
            validatePage();
            return;
        }
    };

    var validateGroup = function (groupName) {
        var group = getValidationGroupByName(groupName);
        if (group !== undefined && group !== null) {
            return !hasErrorInArray(group.registeredFields());
        }

        return undefined;
    };

    var initObservable = function (model, groupName) {
        if (model.errors !== undefined && model.errors !== null) return model;

        model.validationGroup = groupName;
        model.isNumber = ko.observable(false);
        model.errors = ko.observableArray([]);
        model.hasError = ko.computed(function () {
            return model.errors().length > 0;
        });

        model.showMessages = ko.observable(false);
        var updateMessageVisibility = function () {
            var group = getValidationGroupByName(groupName);
            if (group !== undefined && group !== null) {
                return model.showMessages(group.showMessages());
            }

            return model.showMessages(showPageMessages());
        };

        model.message = ko.computed(function () {
            if (!model.showMessages()) return "";

            return concatinateStringArray(model.errors());
        });

        model.clearErrors = function () {
            model.errors.removeAll();
        };

        registerValidationField(model);
        model.subscribe(updateMessageVisibility);

        var group = getValidationGroupByName(groupName);
        if (group !== undefined && group !== null) {
            group.showMessages.subscribe(updateMessageVisibility);
        } else {
            showPageMessages.subscribe(updateMessageVisibility);
        }

        return model;
    };

    var setShowGroupMessages = function (groupName, enabled) {
        var group = getValidationGroupByName(groupName);
        if (group !== undefined && group !== null) {
            group.showMessages(enabled);
            return;
        }
    };

    return {
        dateFormat: dateFormat,
        validatePage: validatePage,
        validateGroup: validateGroup,
        registerValidation: registerValidation,
        initObservable: initObservable,
        hasPageError: hasPageError,
        showPageMessages: showPageMessages,
        setShowGroupMessages: setShowGroupMessages,
        registeredGroups: registeredGroups,
        createConditionalMandatoryParameter: createConditionalMandatoryParameter,
        createConditionalDateParameter: createConditionalDateParameter,
        viewAllErrors: viewAllErrors,
        getDateObjectFromStringOrDate: getDateObjectFromStringOrDate
    };
}();

/* ********************* Custom Extenders ******************** */

ko.extenders.mandatory = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    var errMsgRequired = "This field is required";

    function resetPrevious() {
        target.errors.remove(errMsgRequired);
    }

    function validate(newValue) {
        resetPrevious();

        if (target.isNumber() && (newValue === 0 || newValue === "0")) {
            target.errors.push(errMsgRequired);
        }

        if (newValue === undefined || newValue === null || newValue === "") {
            target.errors.push(errMsgRequired);
        }
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.conditionalMandatory = function (target, options) {
    $.validation.initObservable(target, options.validationGroup);
    var errMsgRequired = "This field is required";

    function resetPrevious() {
        target.errors.remove(errMsgRequired);
    }

    function validateShared(newValue) {
        resetPrevious();

        var actionResult = options.condition();
        if (!actionResult) {
            return true;
        }

        if (target.isNumber() && (newValue === 0 || newValue === "0")) {
            target.errors.push(errMsgRequired);
        }

        if (newValue === undefined || newValue === null || newValue === "") {
            target.errors.push(errMsgRequired);
        }

        return (target.errors.length > 0) ? true : false;
    }

    function validate(newValue) {
        validateShared(newValue);
    }

    function validateObservable() {
        validateShared(target());
        $.validation.validatePage();
    }

    validate(target());
    target.subscribe(validate);
    if (options.conditionalObservable !== undefined) {
        options.conditionalObservable.subscribe(validateObservable);
    }
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.conditionalDate = function (target, options) {
    $.validation.initObservable(target, options.validationGroup);
    var errMsg = (options.customErrorMsg !== undefined) ? options.customErrorMsg : "Please enter a valid date";
    var errMsgFirstPopulated =  "A start date was provided so end date is required";

    function resetPrevious() {
        target.errors.remove(errMsg);
        target.errors.remove(errMsgFirstPopulated);
    }

    function validate(newValue) {
        resetPrevious();
        var hasError = false;

        var newDate = $.validation.getDateObjectFromStringOrDate(newValue);
        var dateToCompare = $.validation.getDateObjectFromStringOrDate((options.hasDateObservableCreteria) ? options.creteria() : options.creteria);

        if (newDate === undefined) {
            if (options.hasDateCreteria || dateToCompare === undefined) {
                return true;
            }
            
            target.errors.push(errMsgFirstPopulated);
            return false;
        }

        if (options.hasDateCreteria || options.hasDateObservableCreteria) {
          

            if (options.isGreaterThan && options.isInclusive) {
                hasError = !(newDate >= dateToCompare);
            }
            else if (options.isGreaterThan && !options.isInclusive) {
                hasError = !(newDate > dateToCompare);
            }
            else if (!options.isGreaterThan && options.isInclusive) {
                hasError = !(newDate <= dateToCompare);
            }
            else if (!options.isGreaterThan && !options.isInclusive) {
                hasError = !(newDate < dateToCompare);
            }
        } else if (options.hasCustomFunctionCreteria) {
            hasError = !options.creteria();
        }

        if (hasError) {
            target.errors.push(errMsg);
        }

        return !hasError;
    }
    
    function validateObservable() {
        validate(target());
        $.validation.validatePage();
    }
    
    validate(target());
    target.subscribe(validate);
    
    if (options.hasDateObservableCreteria) {
        options.creteria.subscribe(validateObservable);
    }
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.boolean = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    var errMsgBoolean = "This field must be true of false";

    target.value = ko.computed(function () {
        if (target() !== undefined && (target() === true || (typeof target() === "string" && target().toLowerCase() === "true"))) {
            return true;
        }
        else if (target() !== undefined && (target() === false || (typeof target() === "string" && (target().toLowerCase() === "false")))) {
            return false;
        }

        return undefined;
    });

    function resetPrevious() {
        target.errors.remove(errMsgBoolean);
    }

    function validate(newValue) {
        resetPrevious();

        if (newValue === undefined || newValue === null || newValue === "") {
            return true;
        }

        if (typeof newValue == "string") {
            newValue = newValue.toLowerCase();
        }

        if (newValue !== true && newValue !== "true" &&
			newValue !== false && newValue !== "false") {
            target.errors.push(errMsgBoolean);
        }

        return null;
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.id_rsa = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    var errMsgRsaId = "Requires a valid South African ID";

    function resetPrevious() {
        target.errors.remove(errMsgRsaId);
    }

    function validate(newValue) {
        resetPrevious();

        if (newValue === undefined || newValue === null || newValue === "") {
            return true;
        }
        var regEx = /^(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))/;

        if (!regEx.test(newValue)) {
            target.errors.push(errMsgRsaId);
        }

        return (target.errors.length > 0) ? true : false;
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.email = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    var errMsgEmail = "Invalid email structure used.";

    function resetPrevious() {
        target.errors.remove(errMsgEmail);
    }

    function validate(newValue) {
        resetPrevious();
        var regEx = /\S+@\S+\.\S+/;

        if (newValue === undefined || newValue === null || newValue === "") {
            return true;
        }

        if (!regEx.test(newValue)) {
            target.errors.push(errMsgEmail);
        }

        return (target.errors.length > 0) ? true : false;
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.amount = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    target.isNumber(true);

    var errMsgDecimal = "Please type in valid amount value e.g. ### ###.##";
    target.value = ko.computed(function () {
        if (typeof target() === "string") {
            return target().replace(/ /g, "");
        }
        return target();
    });

    function resetPrevious() {
        target.errors.remove(errMsgDecimal);
    }

    function validate(newValue) {
        resetPrevious();
        var regEx = /^(\d+\.?\d{0,2}|\.\d{1,2})$/;

        if (typeof newValue === "string") {
            newValue = newValue.replace(/ /g, "");
        }

        if (newValue === undefined || newValue === null || newValue === "") {
            return true;
        }

        if (!regEx.test(newValue)) {
            target.errors.push(errMsgDecimal);
        }

        return (target.errors.length > 0) ? true : false;
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.number = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    target.isNumber(true);

    var errMsgNumber = "Please type in a valid number.";

    function resetPrevious() {
        target.errors.remove(errMsgNumber);
    }

    function validate(newValue) {
        resetPrevious();
        var regEx = /^\d+$/;

        if (newValue === undefined || newValue === null || newValue === "") {
            return true;
        }

        if (!regEx.test(newValue)) {
            target.errors.push(errMsgNumber);
        }

        return (target.errors.length > 0) ? true : false;
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.password = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    var errMsgLength = "Minimum 5 characters";
    var errMsgAplhaNumeric = "Must have numbers and characters";

    function resetPrevious() {
        target.errors.remove(errMsgLength);
        target.errors.remove(errMsgAplhaNumeric);
    }

    function validate(newValue) {
        resetPrevious();
        var foundError = false;

        if (newValue === undefined || newValue === null || newValue === "") {
            return true;
        }

        if (newValue.length < 5) {
            target.errors.push(errMsgLength);
            foundError = true;
        }

        if (!newValue.match(/\d+/g) || !newValue.match(/^[0-9a-zA-Z]+$/)) {
            target.errors.push(errMsgAplhaNumeric);
            foundError = true;
        }

        return foundError;
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.phone = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    var errMsgFormat = "Phone number is an incorrect format (+27 99 999 9999)";
    var errMsgCountry = "Must be a phone number in South Africa";
    target.isInternationalNumber = ko.observable(false);
    target.value = ko.computed(function () {
        if (!target()) {
            return undefined;
        }

        var number = target().replace(/ /g, "");
        if (number.indexOf("0") === 0) {
            number = "+27" + number.substring(1, number.length);
        }

        return number;
    });

    function resetPrevious() {
        target.errors.remove(errMsgFormat);
        target.errors.remove(errMsgCountry);
    }

    function validate(newValue) {
        resetPrevious();
        target.isInternationalNumber(false);

        if (newValue === undefined || newValue === null || newValue === "") {
            return true;
        }

        var cleanedValue = newValue.replace(/ /g, "");
        var isLocalNumber = cleanedValue.length === 10 && cleanedValue.match(/^[0][0-9]+$/);
        if (isLocalNumber) {
            return true;
        }

        var isIntNumber = cleanedValue.length === 12 && (cleanedValue.match(/^[+][0-9]+$/));


        if (isIntNumber) {
            target.isInternationalNumber(true);

            if (cleanedValue.indexOf("+27") !== 0) {

                target.errors.push(errMsgCountry);
                return false;
            }

            return true;
        }

        target.errors.push(errMsgFormat);
        return false;
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

/* Card validation requires jquery.creditCardValidator.js */
ko.extenders.pan = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    var defaultCardType = "Unknown";
    var errMsgCardFormat = "Card number is not in correct format";
    var errMsgIncorrectCard = "Incorrect card used";

    target.validLuhn = ko.observable(false);
    target.cardType = ko.observable(defaultCardType);
    target.value = ko.computed(function () {
        if (!target()) {
            return undefined;
        }

        return target().replace(/-/g, "");
    });

    function resetPrevious() {
        target.errors.remove(errMsgCardFormat);
        target.errors.remove(errMsgIncorrectCard);
    }

    //define a function to do validation
    function validate(newValue) {
        var hasValue = true;
        var isValid = false;

        resetPrevious();

        if (newValue === undefined || newValue === null || newValue === "") {
            hasValue = false;

        } else {
            var elem = $(document.createElement("input")).attr("value", newValue);
            var panResult = $(elem).validateCreditCard({ accept: ["visa", "mastercard"] });

            target.validLuhn(panResult.luhn_valid);
            var cardType = (panResult.card_type !== undefined && panResult.card_type !== null)
                ? panResult.card_type.name
                : defaultCardType;

            target.cardType(cardType);
            isValid = panResult.valid;
        }

        if (hasValue && !target.validLuhn()) {
            target.errors.push(errMsgCardFormat);
        }
        else if (hasValue && !isValid) {
            target.errors.push(errMsgIncorrectCard);
        }
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};

ko.extenders.date = function (target, validationGroup) {
    $.validation.initObservable(target, validationGroup);
    var errMsgFormat = "Date is an incorrect format (" + $.validation.dateFormat + ")";

    target.value = ko.computed(function () {
        if (!target() || target() === "") {
            return undefined;
        }

        return (typeof target() === "string")
           ? moment(target(), $.validation.dateFormat).toDate() : target();
    });

    function resetPrevious() {
        target.errors.remove(errMsgFormat);
    }

    function validate(newValue) {
        resetPrevious();

        if (!newValue) {
            return true;
        }

        var stringDate = (typeof newValue !== "string")
			? moment(target(), $.validation.dateFormat).toDate() : newValue;

        var regEx = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/;
        var stringValue = (typeof newValue === "object") ? moment(newValue).format($.validation.dateFormat) : newValue;

        if (!stringValue.match(regEx)) {
            target.errors.push(errMsgFormat);
        }

        return (target.errors.length > 0) ? true : false;
    }

    validate(target());
    target.subscribe(validate);
    $.validation.registerValidation(target);

    return target;
};


/* ************************ Custom Handlers ************************************* */
// ko.bindingHandlers.datepicker = {
ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor) {
        var divElement = $(element).parent();
        element.readOnly = true;
        $(divElement).datetimepicker({
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-arrow-up",
                down: "fa fa-arrow-down"
            },
            sideBySide: true,
            format: $.validation.dateFormat,
            //showClose: false, 
            ignoreReadonly: true
        });
        ko.utils.registerEventHandler(divElement, "dp.change", function (event) {
            var observable = valueAccessor();
            var currentValue = $(event.target).children("input").val();
            observable(currentValue);
        });

    },
    update: function (element, valueAccessor) {
        var isValueStringType = (typeof valueAccessor()() === "string");
        if (!isValueStringType) {

            if (typeof valueAccessor()() === "object" && valueAccessor()() instanceof Date) {
                element.value = window.site.common.date.dateToString(valueAccessor()());
            } else if (valueAccessor()() === undefined || valueAccessor()() === null) {
                element.value = "";
            } else {
                console.error("Please define a custom date to string in your common JavaScript to use.");
            }
        }
    }
};

ko.bindingHandlers.dateTime = {
    init: function (element, valueAccessor) {
        var divElement = $(element).parent();
        element.readOnly = true;
        $(divElement).datetimepicker({
            sideBySide: true,
            format: "HH:mm",
            ignoreReadonly: true
        });
        ko.utils.registerEventHandler(divElement, "dp.change", function (event) {
            var observable = valueAccessor();
            var currentValue = $(event.target).children("input").val();
            observable(currentValue);
        });

    },
    update: function (element, valueAccessor) {
        var isValueStringType = (typeof valueAccessor()() === "string");

        if (valueAccessor() && !isValueStringType) {

            try {
                element.value = window.site.common.time.dateTimeToString(valueAccessor()());
            }
            catch (err) {
                console.error("Please define a custom date to string in your common JavaScript to use.");
            }
        }
    }
};
