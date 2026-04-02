import 'package:flutter/material.dart';
import 'package:my_api/my_api.dart';

class ApiDemoPage extends StatefulWidget {
  const ApiDemoPage({super.key});

  @override
  State<ApiDemoPage> createState() => _ApiDemoPageState();
}

class _ApiDemoPageState extends State<ApiDemoPage> {
  String _output = 'Press the button to call API';
  bool _loading = false;

  Future<void> _callApi() async {
    setState(() {
      _loading = true;
      _output = 'Calling...';
    });

    try {
      final api = MyApi();
      final resp = await api.getPublicCourseApi().apiPublicCoursesGet();
      final data = resp.data;
      setState(() {
        _output = data == null ? 'No data' : data.toString();
      });
    } catch (e) {
      setState(() {
        _output = 'Error: $e';
      });
    } finally {
      setState(() => _loading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('API Demo')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Text(_output),
            const SizedBox(height: 12),
            ElevatedButton(
              onPressed: _loading ? null : _callApi,
              child: _loading ? const CircularProgressIndicator() : const Text('Call courses'),
            ),
          ],
        ),
      ),
    );
  }
}
