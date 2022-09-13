import { Ibend } from './ibend';
import { IBulan } from './ibulan';
import { IDaftphk3 } from './idaftphk3';
import { IDaftunit } from './idaftunit';
import { IKontrak } from './ikontrak';
import { ISpd } from './ispd';
import { ISppdetb } from './isppdetb';
import { IStattrs } from './istattrs';
import { IZkode } from './izkode';

export interface ISpp {
  idspp : number;
  idunit : number;
  nospp : string;
  kdstatus : string;
  idbulan : number;
  idbend : number;
  idspd : number;
  idphk3 : number;
  idxkode : number;
  noreg : string;
  ketotor : string;
  idkontrak : number;
  keperluan : string;
  penolakan : string;
  tglvalid : null;
  tglspp : null;
  tglaprove: null;
  status : boolean;
  nilaiup: number;
  createdate : null;
  createby : string;
  updatedate : null;
  updateby : string;
  validby : string;
  approveby: string;
  idbendNavigation : Ibend;
  idbulanNavigation : IBulan;
  idkontrakNavigation : IKontrak;
  idphk3Navigation : IDaftphk3;
  idspdNavigation : ISpd;
  sppdetbNavigation : ISppdetb;
  idunitNavigation : IDaftunit;
  idxkodeNavigation : IZkode;
  kdstatusNavigation : IStattrs;
  verifikasi: string;
  valid: boolean;
  idkeg: number;
  validasi: string;
}