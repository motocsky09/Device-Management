import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Device } from '../models/device.model';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private apiUrl = 'http://localhost:5012/api/Device';

  constructor(private http: HttpClient) {}

  getDevices(): Observable<Device[]> {
    return this.http.get<Device[]>(`${this.apiUrl}/GetDevices`);
  }

  getDeviceById(id: string): Observable<Device> {
    return this.http.get<Device>(`${this.apiUrl}/GetDeviceById?id=${id}`);
  }

  createDevice(device: Device): Observable<Device> {
    return this.http.post<Device>(`${this.apiUrl}/CreateDevice`, device);
  }

  updateDevice(device: Device): Observable<Device> {
    return this.http.put<Device>(`${this.apiUrl}/UpdateDevice`, device);
  }

  deleteDevice(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteDevice?id=${id}`);
  }

  assignDevice(deviceId: string, userId: string): Observable<Device> {
    return this.http.put<Device>(`${this.apiUrl}/AssignDevice?deviceId=${deviceId}&userId=${userId}`, {});
  }

  unassignDevice(deviceId: string, userId: string): Observable<Device> {
    return this.http.put<Device>(`${this.apiUrl}/UnassignDevice?deviceId=${deviceId}&userId=${userId}`, {});
  }
  generateDescription(device: Device): Observable<{ description: string }> {
  return this.http.post<{ description: string }>(`${this.apiUrl}/GenerateDescription`, device);
  }
  searchDevices(query: string): Observable<Device[]> {
  return this.http.get<Device[]>(`${this.apiUrl}/Search?query=${query}`);
}
}