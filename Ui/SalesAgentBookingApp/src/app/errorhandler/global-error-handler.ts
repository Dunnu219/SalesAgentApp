import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

  constructor(private injector: Injector) { }
  
  handleError(error: Error | HttpErrorResponse) {

    if (error instanceof HttpErrorResponse) {
      // Server error
      console.log('ErrorHandler: Server side error - ' + error.message);
    } else {
      // Client Error
      console.log('ErrorHandler: Client side error - ' + error.message);
    }
  }
}