import { IWeatherForecast } from "./IWeatherForecast";

export class WeatherForecast implements IWeatherForecast {
  Date: Date;
  Icon: number;
  MinimumValue: number;
  MaximumValue: number;
  Unit: string;
  WindSpeed: string;
  WindUnit: string;
  IconPhrase: string;
  AirQuality: string;
  IsToday: boolean;
  CloudCover: string;

  constructor(){
    this.Date = new Date();
    this.Icon = 0;
    this.MinimumValue = 0;
    this.MaximumValue = 0;
    this.Unit = '';
    this.WindSpeed = '';
    this.WindUnit = '';
    this.IconPhrase = '';
    this.AirQuality = '';
    this.IsToday = false;
    this.CloudCover = '';
  }
}
