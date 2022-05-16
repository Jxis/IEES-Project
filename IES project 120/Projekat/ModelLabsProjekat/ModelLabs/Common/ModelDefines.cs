using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{

	public enum DMSType : short
	{
		MASK_TYPE = unchecked((short)0xFFFF),
		
		//16bitna enumeracije 

		//////// pisemo sve konkretne klase ////////
		///pratimo redosled referenciranja

		DISCONNECTOR								= 0x0001,
		OUTAGESCHEDULE								= 0x0002,
		IRREGULARTIMEPOINT							= 0x0003,
		REGULARINTERVALSCHEDULE						= 0x0004,
		REGULARTIMEPOINT							= 0x0005,
		CURVE										= 0x0006,
		CURVEDATA									= 0x0007,
	}

	[Flags]
	public enum ModelCode : long
	{
		// nasledjivanje        DMS Type          Opis atributa
		//    32-8               16-4                 16-4


		//IdentifiedObject
		IDOBJ										= 0x1000000000000000,
		IDOBJ_GID									= 0x1000000000000104,  // int64
		IDOBJ_DESCRIPTION							= 0x1000000000000207,  // string
		IDOBJ_MRID									= 0x1000000000000307,  // string
		IDOBJ_NAME									= 0x1000000000000407,  // string 

		//BasicIntervalScehule
		BASICINTERVALSCHEDULE						= 0x1100000000000000,
		BASICINTERVALSCHEDULE_STARTTIME				= 0x1100000000000108,  // dateTime
		BASICINTERVALSCHEDULE_VALUE1MULTIPLIER		= 0x110000000000020a,  // enum 
		BASICINTERVALSCHEDULE_VALUE1UNIT			= 0x110000000000030a,  // enum   
		BASICINTERVALSCHEDULE_VALUE2MULTIPLIER		= 0x110000000000040a,  // enum  
		BASICINTERVALSCHEDULE_VALUE2UNIT			= 0x110000000000050a,  // enum

		//IrregularIntervalSchedule
		IRREGULARINTERVALSCHEDULE					= 0x1110000000000000,
		IRREGULARINTERVALSCHEDULE_TIMEPOINTS		= 0x1110000000000119,  // referenca - referenceVector

		//RegularIntervalSchedule
		REGULARINTERVALSCHEDULE						= 0x1120000000040000,
		REGULARINTERVALSCHEDULE_ENDTIME				= 0x1120000000040108,  // dateTime
		REGULARINTERVALSCHEDULE_TIMESTAMP			= 0x1120000000040205,  // float za sekunde
		REGULARINTERVALSCHEDULE_TIMEPOINTS			= 0x1120000000040319,  // referenca - referenceVector

		//IrregularTimePoint
		IRREGULARTIMEPOINT							= 0x1200000000030000,
		IRREGULARTIMEPOINT_INTERVALSCHEDULE			= 0x1200000000030109,  // referenca 
		IRREGULARTIMEPOINT_TIME						= 0x1200000000030205,  // float
		IRREGULARTIMEPOINT_VALUE1					= 0x1200000000030305,  // float
		IRREGULARTIMEPOINT_VALUE2					= 0x1200000000030405,  // float

		//OutageSchedule
		OUTAGESCHEDULE								= 0x1111000000020000,
		OUTAGESCHEDULE_POWERSYSTEMRESOURCE			= 0x1111000000020109,  // reference

		//RegularTimePoint
		REGULARTIMEPOINT							= 0x1300000000050000,
		REGULARTIMEPOINT_INTERVALSCHEDULE			= 0x1300000000050109,  // reference
		REGULARTIMEPOINT_SEQUENENUMBER				= 0x1300000000050203,  // int32
		REGULARTIMEPOINT_VALUE1						= 0x1300000000050305,  // float
		REGULARTIMEPOINT_VALUE2						= 0x1300000000050405,  // float

		//PowerSystemResource
		POWERSYSTEMRESOURCE							= 0x1400000000000000,
		POWERSYSTEMRESOURCE_OUTAGESCHEDULES			= 0x1400000000000119,  // referenca - referenceVector

		//Equipment
		EQUIPMENT									= 0x1410000000000000,
		EQUIPMENT_AGGREGATE							= 0x1410000000000101,  // bool
		EQUIPMENT_NORMALLYINSERVICE					= 0x1410000000000201,  // bool

		// ConductingEquipment
		CONDUCTINGEQUIPMENT							= 0x1411000000000000,

		//Switch
		SWITCH										= 0x1411100000000000,
		SWITCH_NORMALOPEN							= 0x1411100000000101,  //bool
		SWITCH_RATEDCURRENT							= 0x1411100000000205,  //float
		SWITCH_RETAINED								= 0x1411100000000301,  //bool
		SWITCH_SWITCONCOUNT							= 0x1411100000000403,  //int32
		SWITCH_SWITCHONDATE							= 0x1411100000000508,  //dateTime

		//Disconnector
		DISCONNECTOR								= 0x1411110000010000,

		//CurveData
		CURVEDATA									= 0x1600000000070000,
		CURVEDATA_CURVE								= 0x1600000000070109,  //reference 
		CURVEDATA_XVALUE							= 0x1600000000070205,  //float
		CURVEDATA_Y1VALUE							= 0x1600000000070305,  //float
		CURVEDATA_Y2VALUE							= 0x1600000000070405,  //float
		CURVEDATA_Y3VALUE							= 0x1600000000070505,  //float

		//Curve
		CURVE										= 0x1500000000060000,
		CURVE_CURVEDATAS							= 0x1500000000060119,  //referenca - referenceVector
		CURVE_CURVESTYLE							= 0x150000000006020a,  //enum
		CURVE_XMULTIPLIER							= 0x150000000006030a,  //enum
		CURVE_XUNIT									= 0x150000000006040a,  //enum
		CURVE_Y1MULTIPLIER							= 0x150000000006050a,  //enum
		CURVE_Y1UNIT								= 0x150000000006060a,  //enum
		CURVE_Y2MULTIPLIER							= 0x150000000006070a,  //enum
		CURVE_Y2UNIT								= 0x150000000006080a,  //enum
		CURVE_Y3MULTIPLIER							= 0x150000000006090a,  //enum
		CURVE_Y3UNIT								= 0x150000000006100a,  //enum


	}

	[Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}

