/// <reference path="typings/jquery/jquery.d.ts" />
module Timple {

  export function AjaxGET<T>(route: string): JQueryPromise<T> {
    return jQuery.ajax({
      type: 'GET',
      url: this.path,
      dataType: 'json'
    });
  }

  export function AjaxPOST<T>(route: string, bodyObject: any): JQueryPromise<T> {
    return jQuery.ajax({
      type: 'POST',
      url: this.path,
      contentType: 'application/json',
      dataType: 'json',
      data: JSON.stringify(bodyObject),
    });
  }

  export function AjaxDELETE<T>(route: string): JQueryPromise<T> {
    return jQuery.ajax({
      type: 'DELETE',
      url: this.path
    });
  }

  export function AjaxPUT<T>(route: string, bodyObject: any): JQueryPromise<T> {
    return jQuery.ajax({
      type: 'PUT',
      url: this.path,
      contentType: "application/json",
      dataType: 'json',
      data: JSON.stringify(bodyObject)
    });
  }

}