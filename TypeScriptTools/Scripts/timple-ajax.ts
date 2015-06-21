/// <reference path="typings/jquery/jquery.d.ts" />
module Timple {

  export var BasePath: string = '';

  export function AjaxGET<T>(route: string): JQueryPromise<T> {
    return jQuery.ajax({
      type: 'GET',
      url: BasePath + route,
      dataType: 'json'
    });
  }

  export function AjaxPOST<T>(route: string, bodyObject: any): JQueryPromise<T> {
    return jQuery.ajax({
      type: 'POST',
      url: BasePath + route,
      contentType: 'application/json',
      dataType: 'json',
      data: JSON.stringify(bodyObject),
    });
  }

  export function AjaxDELETE<T>(route: string): JQueryPromise<T> {
    return jQuery.ajax({
      type: 'DELETE',
      url: BasePath + route
    });
  }

  export function AjaxPUT<T>(route: string, bodyObject: any): JQueryPromise<T> {
    return jQuery.ajax({
      type: 'PUT',
      url: BasePath + route,
      contentType: "application/json",
      dataType: 'json',
      data: JSON.stringify(bodyObject)
    });
  }

}