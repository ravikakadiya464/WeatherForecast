import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormControl } from '@angular/forms';
import {
  tap,
  debounceTime,
  distinctUntilChanged,
  concatMap,
} from 'rxjs/operators';
import { ICity } from './models/ICity';
import { IWeatherForecast } from './models/IWeatherForecast';
import { WeatherForecastService } from './services/weather-forecast.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})

export class AppComponent implements OnInit, OnDestroy {
  locationSub$: Subscription;
  cities: ICity[];
  locationAutoCompleteControl:FormControl;
  forecasts: IWeatherForecast[];
  index: number;
  selectedCity: string;
  loader: boolean;
  backgroundImage: string;
  background: string;

  constructor(private weatherForecastService: WeatherForecastService) {
    this.locationSub$ = new Subscription();
    this.cities = [];
    this.locationAutoCompleteControl = new FormControl();
    this.forecasts = [];
    this.index = 0;
    this.selectedCity = '';
    this.loader = true;
    this.backgroundImage = '';
    this.background = '';
  }

  locations$: Subscription = new Subscription();

  ngOnInit() {
    this.locationSub$ = this.weatherForecastService
      .getWeatherForCity('202441')
      .subscribe(x => {
        this.setForecasts(x);
        this.loader = false;
        this.backgroundImage = this.forecasts[this.index].IconPhrase.toLowerCase().replace(/ /g, '-').replace(/\//g, '');
        this.background = this.forecasts[this.index].IconPhrase.toLowerCase().replace(/ /g, '-').replace(/\//g, '') + '-bg';
        this.locationAutoCompleteControl.setValue(`Surat, Gujarat, India`)
      });

    this.locations$ = this.locationAutoCompleteControl.valueChanges
      .pipe(
        debounceTime(500),
        distinctUntilChanged(),
        concatMap((name) => (name) ? this.weatherForecastService.getCitiesBySearchTerm(name.split(',')[0]) : this.cities = []),
        tap((data) => {
          if (data.length != 0)
            this.cities = data.map((value: any) => {
              return { key: value?.key, localizedName: `${value.localizedName}, ${value?.administrativeArea?.localizedName}, ${value?.country?.localizedName}` }
            })
        })
      )
      .subscribe();
  }

  displayForecast(value: string) {
    this.selectedCity = value.split(',')[0];

    const selectedLocation: ICity[] = this.cities.filter(
      (option) => option.localizedName === value
    );

    this.locationSub$ = this.weatherForecastService
      .getWeatherForCity(selectedLocation[0].key)
      .subscribe(x => {
        this.setForecasts(x);
      });
  }

  @HostListener('window:keyup.arrowleft', ['$event'])
  keyArrowLeft() {
    if(this.index - 1 >= 0) this.setDay(this.index - 1);
  }

  @HostListener('window:keyup.arrowright', ['$event'])
  keyArrowRight() {
    if(this.index + 1 < this.forecasts.length)
      this.setDay(this.index + 1);
  }

  setDay(i: number) {
    this.index = i;
    this.backgroundImage = this.forecasts[this.index].IconPhrase.toLowerCase().replace(/ /g, '-').replace(/\//g, '')
    this.background = this.forecasts[this.index].IconPhrase.toLowerCase().replace(/ /g, '-').replace(/\//g, '') + '-bg'
  }
  setForecasts(data: any[]) {
    this.forecasts = data.map(x => {
      return {
        Date: x.forecastDate,
        Icon: x?.forecastDetail?.icon,
        MinimumValue: x?.temperature?.minValue,
        MaximumValue: x?.temperature?.maxValue,
        Unit: x?.temperature?.unitMeasure?.unit,
        WindSpeed: x?.forecastDetail?.windSpeed + (" " + x?.forecastDetail?.windSpeedUnit?.unit),
        WindUnit: x?.forecastDetail?.windSpeedUnit?.unit,
        IconPhrase: x?.forecastDetail?.iconPhrase,
        AirQuality: x?.airQuality,
        IsToday: x?.isToday,
        CloudCover: x?.forecastDetail?.cloudCover
      }
    });
    this.backgroundImage = this.forecasts[this.index].IconPhrase.toLowerCase().replace(/ /g, '-').replace(/\//g, '')
    this.background = this.forecasts[this.index].IconPhrase.toLowerCase().replace(/ /g, '-').replace(/\//g, '') + '-bg'
    this.index = this.forecasts.findIndex(x => x.IsToday);
  }

  convetToFahrenheit(isCelsius: boolean) :void {
    if(!isCelsius)
    {
      this.forecasts.forEach(x => {
        x.MinimumValue = Math.round(((x.MinimumValue * 9 / 5) + 32) * 100) / 100;
        x.MaximumValue = Math.round(((x.MaximumValue * 9 / 5) + 32) * 100) / 100;
      })
    }
    else
    {
      this.forecasts.forEach(x => {
        x.MinimumValue = Math.round(((x.MinimumValue - 32) * 5 / 9) * 100) / 100;
        x.MaximumValue = Math.round(((x.MaximumValue - 32) * 5 / 9) * 100) / 100;
      })
    }
  }

  ngOnDestroy() {
    this.locationSub$.unsubscribe();
    this.locations$.unsubscribe();
  }
}

