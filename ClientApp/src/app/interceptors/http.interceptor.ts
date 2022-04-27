import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';

import {catchError, Observable, of} from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class HttpClientInterceptor implements HttpInterceptor {

  constructor(private snackBar: MatSnackBar) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError((error, caught) => {
        this.handleError(error);
        return of(error);
      }) as any);
  }

  private handleError(err: HttpErrorResponse): Observable<any> {
    if (err.status === 400 || err.status === 500) {
      this.snackBar.open(err?.error?.message , 'Close',
      {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'end',
          panelClass: 'red-snackbar',
      });
      return of(err.message);
    }
    else {
      this.snackBar.open("Something went wrong!!!" , 'Close',
      {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'end',
          panelClass: 'red-snackbar',
      });
      return of("Something went wrong!!!");
    }
    
    throw err;
  }
}