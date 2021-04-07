import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { User } from '../models';
import { UtilitiesService } from './utilities.service';



@Injectable({ providedIn: 'root' })
export class UsersService extends BaseService {
    constructor(
        private http: HttpClient,
        private UtilitiesService: UtilitiesService) {
        super();
    }
    getAll() {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        return this.http.get<User[]>(`${environment.apiUrl}/api/vn/users`, httpOptions)
            .pipe(catchError(this.handleError));
    }
    getMenuByUser(userId: string) {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        return this.http.get<Function[]>(`${environment.apiUrl}/api/vn/users/${userId}/menu`, httpOptions)
            .pipe(map(response => {
                const funtions = this.UtilitiesService.UnflatteringForLeftMenu(response);
                return funtions;
            }), catchError(this.handleError));
    }
}