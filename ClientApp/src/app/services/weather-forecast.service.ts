import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { delay } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class WeatherForecastService {
  constructor(private http: HttpClient) { }

  getCitiesBySearchTerm(city: string): Observable<any> {
    const path = `${environment.baseUrl}/cities?city=${city}`;
    return this.http.get<any>(path).pipe(
      delay(500)
    );
  }

  getWeatherForCity(key: string): Observable<any[]> {
    const path = `${environment.baseUrl}/cities/${key}/weather-forecasts`;
    return this.http.get<any>(path).pipe(
      delay(500)
    );
  }
}
