import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';


export class UsingProductsPlaces {
  constructor(
    public Name: string,
    public SecondName: string,
    public Login: string,
    public InventoryNumber: number,
    public NumberStay: number,
    public NumberLayer: string,
    public DateOfStart: string
  ) {}
}

@Injectable({
  providedIn: 'root'
})
export class WarehousemanPageService {

  private apiUrl = 'http://localhost:5001/usingProductsPlaces';

  private page: number = 1;
  private perPage: number = 2;
  private columns: string[] = ['InventoryNumber', 'Login', 'Name', "SecondName", "NumberStay", "NumberLayer", "DateOfStart"];

  constructor(private http: HttpClient) {}

  getColumns(): string[]
  {
    return this.columns;
  }

  getUsingProductsPlaces(): Observable<{ items: UsingProductsPlaces[]; pages: number }> {
    const params = new HttpParams()
      .set('page', this.page ? this.page.toString() : '')
      .set('per_page', this.perPage ? this.perPage.toString() : '');

    const headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('id_token') });

    return this.http.get<{ results: any[]; total: number; per_page: number }>(this.apiUrl, { headers, params }).pipe(
      map((data) => ({
        items: data.results.map((itemData: any) => new UsingProductsPlaces(
          itemData.name,
          itemData.secondName,
          itemData.login,
          itemData.inventoryNumber,
          itemData.numberStay,
          itemData.numberLayer,
          itemData.dateOfStart
        )),
        pages: Math.ceil(data.total / data.per_page),
      })),
      catchError(error => {
        console.error('Error:', error);
        return throwError(error);
      })
    );
  }

  getUsingProductsPlaceById(id: string): Observable<any> {
    const headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('id_token') });

    return this.http.get(`${this.apiUrl}/${id}`, { headers });
  }

  deleteUsingProductsPlaceById(id: string): Observable<any> {
    const headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('id_token') });
    return this.http.delete(`${this.apiUrl}/${id}`, { headers });
  }

  onGoTo(page: number): number {
    this.page = page;
    return this.page;
  }

  onNext(): number {
    this.page = this.page + 1;
    return this.page;
  }

  onPrevious(): number {
    this.page = this.page - 1;
    return this.page;
  }
}
