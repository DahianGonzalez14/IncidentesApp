import { Component, OnInit } from '@angular/core';
import { Departamento } from 'src/app/models/Departamento.model';
import { Incidente } from 'src/app/models/Incidente.model';
import { Prioridad } from 'src/app/models/Prioridad.mode';
import { Usuario } from 'src/app/models/Usuario.model';
import { AdministracionService } from 'src/app/services/administracion.service';
import { IncidenteService } from 'src/app/services/incidente.service';
import Swal from 'sweetalert2';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-historial-solicitudes',
  templateUrl: './historial-solicitudes.component.html',
  styleUrls: ['./historial-solicitudes.component.css']
})
export class HistorialSolicitudesComponent implements OnInit {
  public idUsuarioSesion: number;
  public listadoHistorialIncidente: Incidente[] = [];
  public listadoTempHistorialIncidente: Incidente[] = [];
  public hoverShowed: boolean = true;
  public loading: boolean = false;
  public listadoUsuarios: Usuario[] = [];
  public listadoPrioridades: Prioridad[] = [];
  public listadoDepartamentos: Departamento[] = [];
  fileName = 'HistorialSolicitudes.xlsx';  
  public isfull: boolean = false;

  constructor(private admintracionService: AdministracionService, private incidenteService: IncidenteService) { }

  ngOnInit() {
    this.getUsuarios();
    this.getDepartamentos();
    this.getPrioridades();
    this.idUsuarioSesion = JSON.parse(localStorage.getItem('user'))?.usuarioId;
    this.getHistorialSolicitudesByUser();
  }

  getHistorialSolicitudesByUser(){
    this.loading = true;
    this.incidenteService.getHistorialSolicitudesByUser(this.idUsuarioSesion).subscribe( (res: Incidente[]) => {
      this.listadoHistorialIncidente = res;
      this.listadoTempHistorialIncidente = this.listadoHistorialIncidente;
      this.loading = false;
      if(this.listadoHistorialIncidente.length > 0){
        this.isfull = true;
        console.log("entro");
      }
    });
  }

  buscar( termino: string){
    let arrayToReturn: any[] = [];
    termino = termino.toLowerCase();
    this.loading = true;
    for ( let i = 0; i < this.listadoTempHistorialIncidente.length; i++) {
      let incidente = this.listadoTempHistorialIncidente[i];
      let titulo = incidente.titulo.toLowerCase();
      if (titulo.indexOf(termino) >= 0) {
        arrayToReturn.push(incidente);
      }
    }
    this.listadoHistorialIncidente = arrayToReturn;
    this.loading = false;
    if(this.listadoHistorialIncidente.length == 0){
      this.hoverShowed = false;
    } else{
      this.hoverShowed = true;
    }
  }

  getUsuarios(){
    this.admintracionService.getUsers().subscribe( (res: Usuario[]) => {
      this.listadoUsuarios = res;
    });
  }

  getPrioridades(){
    this.admintracionService.getPrioridades().subscribe( (res: Prioridad[]) => {
      this.listadoPrioridades = res;
    });
  }

  getDepartamentos(){
    this.admintracionService.getDepartamentos().subscribe( (res: Departamento[]) => {
      this.listadoDepartamentos = res;
    });
  }

  getNombreStatusByCode(code: string){
    if(code == '0'){
      return 'Activo';
    }else{
      return 'Inactivo';
    }
  }

  getNombreUsuarioReportaById(id: any){
    if(id == null){
      return 'Sin Asignar';
    }else{
      return this.listadoUsuarios.find( x => x.usuarioId == id)?.nombreUsuario;
    }
  }

  getNombreUsuarioAsignadoById(id: any){

    if(id == null){
      return 'Sin Asignar';
    }else{
      return this.listadoUsuarios.find( x => x.usuarioId == id)?.nombreUsuario;
    }
  }

  getPrioridadById(id: any){
    if(id == null){
      return 'Sin Asignar';
    }else{
      return this.listadoPrioridades.find( x => x.prioridadId == id)?.nombre;
    }
  }

  getDepartamentoById(id: any){
    if(id == null){
      return 'Sin Asignar';
    }else{
      return this.listadoDepartamentos.find( x => x.departamentoId == id)?.nombre;
    }
  }

  exportExcel(){
      /* table id is passed over here */   
      let element = document.getElementById('historialIncidente'); 
      const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'historico');

      /* save to file */
      XLSX.writeFile(wb, this.fileName);
  }

  needSeeButton(descripcion: string){
    let cantidadCaracteres = descripcion.length;
    if(cantidadCaracteres > 49){
      return true;
    }else{
      return false;
    }
  }

  verMas(descripcion: string){
    Swal.fire({
      title: descripcion
    });
  }


}
