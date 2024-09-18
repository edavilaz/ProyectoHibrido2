; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [128 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [256 x i32] [
	i32 42639949, ; 0: System.Threading.Thread => 0x28aa24d => 118
	i32 67008169, ; 1: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 33
	i32 72070932, ; 2: Microsoft.Maui.Graphics.dll => 0x44bb714 => 52
	i32 98325684, ; 3: Microsoft.Extensions.Diagnostics.Abstractions => 0x5dc54b4 => 42
	i32 117431740, ; 4: System.Runtime.InteropServices => 0x6ffddbc => 111
	i32 165246403, ; 5: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 62
	i32 182336117, ; 6: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 80
	i32 195452805, ; 7: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 30
	i32 199333315, ; 8: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 31
	i32 205061960, ; 9: System.ComponentModel => 0xc38ff48 => 93
	i32 221958352, ; 10: Microsoft.Extensions.Diagnostics.dll => 0xd3ad0d0 => 41
	i32 280992041, ; 11: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 2
	i32 291275502, ; 12: Microsoft.Extensions.Http.dll => 0x115c82ee => 43
	i32 317674968, ; 13: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 30
	i32 318968648, ; 14: Xamarin.AndroidX.Activity.dll => 0x13031348 => 58
	i32 336156722, ; 15: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 15
	i32 342366114, ; 16: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 69
	i32 347068432, ; 17: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 56
	i32 356389973, ; 18: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 14
	i32 379916513, ; 19: System.Threading.Thread.dll => 0x16a510e1 => 118
	i32 385762202, ; 20: System.Memory.dll => 0x16fe439a => 101
	i32 395744057, ; 21: _Microsoft.Android.Resource.Designer => 0x17969339 => 34
	i32 435591531, ; 22: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 26
	i32 442565967, ; 23: System.Collections => 0x1a61054f => 90
	i32 450948140, ; 24: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 68
	i32 456227837, ; 25: System.Web.HttpUtility.dll => 0x1b317bfd => 120
	i32 469710990, ; 26: System.dll => 0x1bff388e => 122
	i32 498788369, ; 27: System.ObjectModel => 0x1dbae811 => 107
	i32 500358224, ; 28: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 13
	i32 503918385, ; 29: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 7
	i32 513247710, ; 30: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 47
	i32 539058512, ; 31: Microsoft.Extensions.Logging => 0x20216150 => 44
	i32 592146354, ; 32: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 21
	i32 597488923, ; 33: CommunityToolkit.Maui => 0x239cf51b => 35
	i32 627609679, ; 34: Xamarin.AndroidX.CustomView => 0x2568904f => 66
	i32 627931235, ; 35: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 19
	i32 662205335, ; 36: System.Text.Encodings.Web.dll => 0x27787397 => 115
	i32 672442732, ; 37: System.Collections.Concurrent => 0x2814a96c => 87
	i32 688181140, ; 38: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 1
	i32 706645707, ; 39: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 16
	i32 709557578, ; 40: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 4
	i32 722857257, ; 41: System.Runtime.Loader.dll => 0x2b15ed29 => 112
	i32 748832960, ; 42: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 54
	i32 759454413, ; 43: System.Net.Requests => 0x2d445acd => 105
	i32 775507847, ; 44: System.IO.Compression => 0x2e394f87 => 98
	i32 777317022, ; 45: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 25
	i32 789151979, ; 46: Microsoft.Extensions.Options => 0x2f0980eb => 46
	i32 823281589, ; 47: System.Private.Uri.dll => 0x311247b5 => 108
	i32 830298997, ; 48: System.IO.Compression.Brotli => 0x317d5b75 => 97
	i32 904024072, ; 49: System.ComponentModel.Primitives.dll => 0x35e25008 => 91
	i32 926902833, ; 50: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 28
	i32 967690846, ; 51: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 69
	i32 992768348, ; 52: System.Collections.dll => 0x3b2c715c => 90
	i32 1012816738, ; 53: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 79
	i32 1028951442, ; 54: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 40
	i32 1029334545, ; 55: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 3
	i32 1035644815, ; 56: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 59
	i32 1044663988, ; 57: System.Linq.Expressions.dll => 0x3e444eb4 => 99
	i32 1048992957, ; 58: Microsoft.Extensions.Diagnostics.Abstractions.dll => 0x3e865cbd => 42
	i32 1052210849, ; 59: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 71
	i32 1082857460, ; 60: System.ComponentModel.TypeConverter => 0x408b17f4 => 92
	i32 1084122840, ; 61: Xamarin.Kotlin.StdLib => 0x409e66d8 => 84
	i32 1086696846, ; 62: ProyectoHibrido2.dll => 0x40c5ad8e => 86
	i32 1098259244, ; 63: System => 0x41761b2c => 122
	i32 1118262833, ; 64: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 16
	i32 1168523401, ; 65: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 22
	i32 1178241025, ; 66: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 76
	i32 1203215381, ; 67: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 20
	i32 1234928153, ; 68: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 18
	i32 1260983243, ; 69: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 2
	i32 1292207520, ; 70: SQLitePCLRaw.core.dll => 0x4d0585a0 => 55
	i32 1293217323, ; 71: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 67
	i32 1324164729, ; 72: System.Linq => 0x4eed2679 => 100
	i32 1373134921, ; 73: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 32
	i32 1376866003, ; 74: Xamarin.AndroidX.SavedState => 0x52114ed3 => 79
	i32 1406073936, ; 75: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 63
	i32 1430672901, ; 76: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 0
	i32 1461004990, ; 77: es\Microsoft.Maui.Controls.resources => 0x57152abe => 6
	i32 1461234159, ; 78: System.Collections.Immutable.dll => 0x5718a9ef => 88
	i32 1462112819, ; 79: System.IO.Compression.dll => 0x57261233 => 98
	i32 1469204771, ; 80: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 60
	i32 1470490898, ; 81: Microsoft.Extensions.Primitives => 0x57a5e912 => 47
	i32 1479771757, ; 82: System.Collections.Immutable => 0x5833866d => 88
	i32 1480492111, ; 83: System.IO.Compression.Brotli.dll => 0x583e844f => 97
	i32 1493001747, ; 84: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 10
	i32 1505131794, ; 85: Microsoft.Extensions.Http => 0x59b67d12 => 43
	i32 1514721132, ; 86: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 5
	i32 1543031311, ; 87: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 117
	i32 1551623176, ; 88: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 25
	i32 1622152042, ; 89: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 73
	i32 1624863272, ; 90: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 82
	i32 1634654947, ; 91: CommunityToolkit.Maui.Core.dll => 0x616edae3 => 36
	i32 1636350590, ; 92: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 65
	i32 1639515021, ; 93: System.Net.Http.dll => 0x61b9038d => 102
	i32 1639986890, ; 94: System.Text.RegularExpressions => 0x61c036ca => 117
	i32 1657153582, ; 95: System.Runtime => 0x62c6282e => 113
	i32 1658251792, ; 96: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 83
	i32 1677501392, ; 97: System.Net.Primitives.dll => 0x63fca3d0 => 104
	i32 1679769178, ; 98: System.Security.Cryptography => 0x641f3e5a => 114
	i32 1711441057, ; 99: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 56
	i32 1729485958, ; 100: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 61
	i32 1736233607, ; 101: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 23
	i32 1743415430, ; 102: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 1
	i32 1763938596, ; 103: System.Diagnostics.TraceSource.dll => 0x69239124 => 96
	i32 1766324549, ; 104: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 80
	i32 1770582343, ; 105: Microsoft.Extensions.Logging.dll => 0x6988f147 => 44
	i32 1780572499, ; 106: Mono.Android.Runtime.dll => 0x6a216153 => 126
	i32 1782862114, ; 107: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 17
	i32 1788241197, ; 108: Xamarin.AndroidX.Fragment => 0x6a96652d => 68
	i32 1793755602, ; 109: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 9
	i32 1808609942, ; 110: Xamarin.AndroidX.Loader => 0x6bcd3296 => 73
	i32 1813058853, ; 111: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 84
	i32 1813201214, ; 112: Xamarin.Google.Android.Material => 0x6c13413e => 83
	i32 1818569960, ; 113: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 77
	i32 1828688058, ; 114: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 45
	i32 1842015223, ; 115: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 29
	i32 1853025655, ; 116: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 26
	i32 1858542181, ; 117: System.Linq.Expressions => 0x6ec71a65 => 99
	i32 1875935024, ; 118: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 8
	i32 1910275211, ; 119: System.Collections.NonGeneric.dll => 0x71dc7c8b => 89
	i32 1968388702, ; 120: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 37
	i32 2003115576, ; 121: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 5
	i32 2019465201, ; 122: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 71
	i32 2025202353, ; 123: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 0
	i32 2045470958, ; 124: System.Private.Xml => 0x79eb68ee => 109
	i32 2055257422, ; 125: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 70
	i32 2066184531, ; 126: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 4
	i32 2070888862, ; 127: System.Diagnostics.TraceSource => 0x7b6f419e => 96
	i32 2079903147, ; 128: System.Runtime.dll => 0x7bf8cdab => 113
	i32 2090596640, ; 129: System.Numerics.Vectors => 0x7c9bf920 => 106
	i32 2103459038, ; 130: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 57
	i32 2127167465, ; 131: System.Console => 0x7ec9ffe9 => 94
	i32 2159891885, ; 132: Microsoft.Maui => 0x80bd55ad => 50
	i32 2169148018, ; 133: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 12
	i32 2181898931, ; 134: Microsoft.Extensions.Options.dll => 0x820d22b3 => 46
	i32 2192057212, ; 135: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 45
	i32 2193016926, ; 136: System.ObjectModel.dll => 0x82b6c85e => 107
	i32 2201107256, ; 137: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 85
	i32 2201231467, ; 138: System.Net.Http => 0x8334206b => 102
	i32 2207618523, ; 139: it\Microsoft.Maui.Controls.resources => 0x839595db => 14
	i32 2266799131, ; 140: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 38
	i32 2270573516, ; 141: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 8
	i32 2279755925, ; 142: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 78
	i32 2298471582, ; 143: System.Net.Mail => 0x88ffe49e => 103
	i32 2303942373, ; 144: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 18
	i32 2305521784, ; 145: System.Private.CoreLib.dll => 0x896b7878 => 124
	i32 2340441535, ; 146: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 110
	i32 2353062107, ; 147: System.Net.Primitives => 0x8c40e0db => 104
	i32 2368005991, ; 148: System.Xml.ReaderWriter.dll => 0x8d24e767 => 121
	i32 2371007202, ; 149: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 37
	i32 2395872292, ; 150: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 13
	i32 2401565422, ; 151: System.Web.HttpUtility => 0x8f24faee => 120
	i32 2427813419, ; 152: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 10
	i32 2435356389, ; 153: System.Console.dll => 0x912896e5 => 94
	i32 2465273461, ; 154: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 54
	i32 2471841756, ; 155: netstandard.dll => 0x93554fdc => 123
	i32 2475788418, ; 156: Java.Interop.dll => 0x93918882 => 125
	i32 2480646305, ; 157: Microsoft.Maui.Controls => 0x93dba8a1 => 48
	i32 2550873716, ; 158: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 11
	i32 2570120770, ; 159: System.Text.Encodings.Web => 0x9930ee42 => 115
	i32 2593496499, ; 160: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 20
	i32 2605712449, ; 161: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 85
	i32 2617129537, ; 162: System.Private.Xml.dll => 0x9bfe3a41 => 109
	i32 2620871830, ; 163: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 65
	i32 2626831493, ; 164: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 15
	i32 2663698177, ; 165: System.Runtime.Loader => 0x9ec4cf01 => 112
	i32 2732626843, ; 166: Xamarin.AndroidX.Activity => 0xa2e0939b => 58
	i32 2737747696, ; 167: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 60
	i32 2752995522, ; 168: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 21
	i32 2758225723, ; 169: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 49
	i32 2764765095, ; 170: Microsoft.Maui.dll => 0xa4caf7a7 => 50
	i32 2778768386, ; 171: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 81
	i32 2785988530, ; 172: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 27
	i32 2801831435, ; 173: Microsoft.Maui.Graphics => 0xa7008e0b => 52
	i32 2806116107, ; 174: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 6
	i32 2810250172, ; 175: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 63
	i32 2831556043, ; 176: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 19
	i32 2853208004, ; 177: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 81
	i32 2861189240, ; 178: Microsoft.Maui.Essentials => 0xaa8a4878 => 51
	i32 2868488919, ; 179: CommunityToolkit.Maui.Core => 0xaaf9aad7 => 36
	i32 2909740682, ; 180: System.Private.CoreLib => 0xad6f1e8a => 124
	i32 2916838712, ; 181: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 82
	i32 2919462931, ; 182: System.Numerics.Vectors.dll => 0xae037813 => 106
	i32 2959614098, ; 183: System.ComponentModel.dll => 0xb0682092 => 93
	i32 2978675010, ; 184: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 67
	i32 3020703001, ; 185: Microsoft.Extensions.Diagnostics => 0xb40c4519 => 41
	i32 3038032645, ; 186: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 34
	i32 3057625584, ; 187: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 74
	i32 3059408633, ; 188: Mono.Android.Runtime => 0xb65adef9 => 126
	i32 3059793426, ; 189: System.ComponentModel.Primitives => 0xb660be12 => 91
	i32 3077302341, ; 190: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 12
	i32 3178803400, ; 191: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 75
	i32 3220365878, ; 192: System.Threading => 0xbff2e236 => 119
	i32 3258312781, ; 193: Xamarin.AndroidX.CardView => 0xc235e84d => 61
	i32 3286872994, ; 194: SQLite-net.dll => 0xc3e9b3a2 => 53
	i32 3305363605, ; 195: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 7
	i32 3316684772, ; 196: System.Net.Requests.dll => 0xc5b097e4 => 105
	i32 3317135071, ; 197: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 66
	i32 3346324047, ; 198: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 76
	i32 3357674450, ; 199: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 24
	i32 3358260929, ; 200: System.Text.Json => 0xc82afec1 => 116
	i32 3360279109, ; 201: SQLitePCLRaw.core => 0xc849ca45 => 55
	i32 3362522851, ; 202: Xamarin.AndroidX.Core => 0xc86c06e3 => 64
	i32 3366347497, ; 203: Java.Interop => 0xc8a662e9 => 125
	i32 3374999561, ; 204: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 78
	i32 3381016424, ; 205: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 3
	i32 3428513518, ; 206: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 39
	i32 3430777524, ; 207: netstandard => 0xcc7d82b4 => 123
	i32 3463511458, ; 208: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 11
	i32 3471940407, ; 209: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 92
	i32 3476120550, ; 210: Mono.Android => 0xcf3163e6 => 127
	i32 3479583265, ; 211: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 24
	i32 3484440000, ; 212: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 23
	i32 3485117614, ; 213: System.Text.Json.dll => 0xcfbaacae => 116
	i32 3580758918, ; 214: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 31
	i32 3608519521, ; 215: System.Linq.dll => 0xd715a361 => 100
	i32 3624195450, ; 216: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 110
	i32 3641597786, ; 217: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 70
	i32 3643446276, ; 218: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 28
	i32 3643854240, ; 219: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 75
	i32 3657292374, ; 220: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 38
	i32 3672681054, ; 221: Mono.Android.dll => 0xdae8aa5e => 127
	i32 3697841164, ; 222: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 33
	i32 3724971120, ; 223: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 74
	i32 3748608112, ; 224: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 95
	i32 3754567612, ; 225: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 57
	i32 3781692046, ; 226: ProyectoHibrido2 => 0xe1680a8e => 86
	i32 3786282454, ; 227: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 62
	i32 3792276235, ; 228: System.Collections.NonGeneric => 0xe2098b0b => 89
	i32 3817368567, ; 229: CommunityToolkit.Maui.dll => 0xe3886bf7 => 35
	i32 3823082795, ; 230: System.Security.Cryptography.dll => 0xe3df9d2b => 114
	i32 3841636137, ; 231: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 40
	i32 3844307129, ; 232: System.Net.Mail.dll => 0xe52378b9 => 103
	i32 3849253459, ; 233: System.Runtime.InteropServices.dll => 0xe56ef253 => 111
	i32 3876362041, ; 234: SQLite-net => 0xe70c9739 => 53
	i32 3889960447, ; 235: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 32
	i32 3896106733, ; 236: System.Collections.Concurrent.dll => 0xe839deed => 87
	i32 3896760992, ; 237: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 64
	i32 3928044579, ; 238: System.Xml.ReaderWriter => 0xea213423 => 121
	i32 3931092270, ; 239: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 77
	i32 3955647286, ; 240: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 59
	i32 3980434154, ; 241: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 27
	i32 3987592930, ; 242: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 9
	i32 4025784931, ; 243: System.Memory => 0xeff49a63 => 101
	i32 4046471985, ; 244: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 49
	i32 4073602200, ; 245: System.Threading.dll => 0xf2ce3c98 => 119
	i32 4094352644, ; 246: Microsoft.Maui.Essentials.dll => 0xf40add04 => 51
	i32 4100113165, ; 247: System.Private.Uri => 0xf462c30d => 108
	i32 4102112229, ; 248: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 22
	i32 4125707920, ; 249: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 17
	i32 4126470640, ; 250: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 39
	i32 4150914736, ; 251: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 29
	i32 4182413190, ; 252: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 72
	i32 4213026141, ; 253: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 95
	i32 4271975918, ; 254: Microsoft.Maui.Controls.dll => 0xfea12dee => 48
	i32 4292120959 ; 255: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 72
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [256 x i32] [
	i32 118, ; 0
	i32 33, ; 1
	i32 52, ; 2
	i32 42, ; 3
	i32 111, ; 4
	i32 62, ; 5
	i32 80, ; 6
	i32 30, ; 7
	i32 31, ; 8
	i32 93, ; 9
	i32 41, ; 10
	i32 2, ; 11
	i32 43, ; 12
	i32 30, ; 13
	i32 58, ; 14
	i32 15, ; 15
	i32 69, ; 16
	i32 56, ; 17
	i32 14, ; 18
	i32 118, ; 19
	i32 101, ; 20
	i32 34, ; 21
	i32 26, ; 22
	i32 90, ; 23
	i32 68, ; 24
	i32 120, ; 25
	i32 122, ; 26
	i32 107, ; 27
	i32 13, ; 28
	i32 7, ; 29
	i32 47, ; 30
	i32 44, ; 31
	i32 21, ; 32
	i32 35, ; 33
	i32 66, ; 34
	i32 19, ; 35
	i32 115, ; 36
	i32 87, ; 37
	i32 1, ; 38
	i32 16, ; 39
	i32 4, ; 40
	i32 112, ; 41
	i32 54, ; 42
	i32 105, ; 43
	i32 98, ; 44
	i32 25, ; 45
	i32 46, ; 46
	i32 108, ; 47
	i32 97, ; 48
	i32 91, ; 49
	i32 28, ; 50
	i32 69, ; 51
	i32 90, ; 52
	i32 79, ; 53
	i32 40, ; 54
	i32 3, ; 55
	i32 59, ; 56
	i32 99, ; 57
	i32 42, ; 58
	i32 71, ; 59
	i32 92, ; 60
	i32 84, ; 61
	i32 86, ; 62
	i32 122, ; 63
	i32 16, ; 64
	i32 22, ; 65
	i32 76, ; 66
	i32 20, ; 67
	i32 18, ; 68
	i32 2, ; 69
	i32 55, ; 70
	i32 67, ; 71
	i32 100, ; 72
	i32 32, ; 73
	i32 79, ; 74
	i32 63, ; 75
	i32 0, ; 76
	i32 6, ; 77
	i32 88, ; 78
	i32 98, ; 79
	i32 60, ; 80
	i32 47, ; 81
	i32 88, ; 82
	i32 97, ; 83
	i32 10, ; 84
	i32 43, ; 85
	i32 5, ; 86
	i32 117, ; 87
	i32 25, ; 88
	i32 73, ; 89
	i32 82, ; 90
	i32 36, ; 91
	i32 65, ; 92
	i32 102, ; 93
	i32 117, ; 94
	i32 113, ; 95
	i32 83, ; 96
	i32 104, ; 97
	i32 114, ; 98
	i32 56, ; 99
	i32 61, ; 100
	i32 23, ; 101
	i32 1, ; 102
	i32 96, ; 103
	i32 80, ; 104
	i32 44, ; 105
	i32 126, ; 106
	i32 17, ; 107
	i32 68, ; 108
	i32 9, ; 109
	i32 73, ; 110
	i32 84, ; 111
	i32 83, ; 112
	i32 77, ; 113
	i32 45, ; 114
	i32 29, ; 115
	i32 26, ; 116
	i32 99, ; 117
	i32 8, ; 118
	i32 89, ; 119
	i32 37, ; 120
	i32 5, ; 121
	i32 71, ; 122
	i32 0, ; 123
	i32 109, ; 124
	i32 70, ; 125
	i32 4, ; 126
	i32 96, ; 127
	i32 113, ; 128
	i32 106, ; 129
	i32 57, ; 130
	i32 94, ; 131
	i32 50, ; 132
	i32 12, ; 133
	i32 46, ; 134
	i32 45, ; 135
	i32 107, ; 136
	i32 85, ; 137
	i32 102, ; 138
	i32 14, ; 139
	i32 38, ; 140
	i32 8, ; 141
	i32 78, ; 142
	i32 103, ; 143
	i32 18, ; 144
	i32 124, ; 145
	i32 110, ; 146
	i32 104, ; 147
	i32 121, ; 148
	i32 37, ; 149
	i32 13, ; 150
	i32 120, ; 151
	i32 10, ; 152
	i32 94, ; 153
	i32 54, ; 154
	i32 123, ; 155
	i32 125, ; 156
	i32 48, ; 157
	i32 11, ; 158
	i32 115, ; 159
	i32 20, ; 160
	i32 85, ; 161
	i32 109, ; 162
	i32 65, ; 163
	i32 15, ; 164
	i32 112, ; 165
	i32 58, ; 166
	i32 60, ; 167
	i32 21, ; 168
	i32 49, ; 169
	i32 50, ; 170
	i32 81, ; 171
	i32 27, ; 172
	i32 52, ; 173
	i32 6, ; 174
	i32 63, ; 175
	i32 19, ; 176
	i32 81, ; 177
	i32 51, ; 178
	i32 36, ; 179
	i32 124, ; 180
	i32 82, ; 181
	i32 106, ; 182
	i32 93, ; 183
	i32 67, ; 184
	i32 41, ; 185
	i32 34, ; 186
	i32 74, ; 187
	i32 126, ; 188
	i32 91, ; 189
	i32 12, ; 190
	i32 75, ; 191
	i32 119, ; 192
	i32 61, ; 193
	i32 53, ; 194
	i32 7, ; 195
	i32 105, ; 196
	i32 66, ; 197
	i32 76, ; 198
	i32 24, ; 199
	i32 116, ; 200
	i32 55, ; 201
	i32 64, ; 202
	i32 125, ; 203
	i32 78, ; 204
	i32 3, ; 205
	i32 39, ; 206
	i32 123, ; 207
	i32 11, ; 208
	i32 92, ; 209
	i32 127, ; 210
	i32 24, ; 211
	i32 23, ; 212
	i32 116, ; 213
	i32 31, ; 214
	i32 100, ; 215
	i32 110, ; 216
	i32 70, ; 217
	i32 28, ; 218
	i32 75, ; 219
	i32 38, ; 220
	i32 127, ; 221
	i32 33, ; 222
	i32 74, ; 223
	i32 95, ; 224
	i32 57, ; 225
	i32 86, ; 226
	i32 62, ; 227
	i32 89, ; 228
	i32 35, ; 229
	i32 114, ; 230
	i32 40, ; 231
	i32 103, ; 232
	i32 111, ; 233
	i32 53, ; 234
	i32 32, ; 235
	i32 87, ; 236
	i32 64, ; 237
	i32 121, ; 238
	i32 77, ; 239
	i32 59, ; 240
	i32 27, ; 241
	i32 9, ; 242
	i32 101, ; 243
	i32 49, ; 244
	i32 119, ; 245
	i32 51, ; 246
	i32 108, ; 247
	i32 22, ; 248
	i32 17, ; 249
	i32 39, ; 250
	i32 29, ; 251
	i32 72, ; 252
	i32 95, ; 253
	i32 48, ; 254
	i32 72 ; 255
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 96b6bb65e8736e45180905177aa343f0e1854ea3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
